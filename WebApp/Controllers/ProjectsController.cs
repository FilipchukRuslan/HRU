using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IImageManager imageManager;
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly IProjectsManager projectsManager;

        public ProjectsController(INewsManager newsManager, IImageManager imageManager, IHostingEnvironment appEnvironment, IProjectsManager projectsManager)
        {
            this.imageManager = imageManager;
            this.appEnvironment = appEnvironment;
            this.projectsManager = projectsManager;
        }

        public IActionResult Index()
        {
            try
            {
                var projects = projectsManager.GetAll().ToList();
                var images = imageManager.GetAll().ToList();

                return View(new ProjectsViewModel()
                {
                    ProjectsLst = projects,
                    ImageLst = images
                });
            }
            catch (Exception ex)
            {

            }
            return View();

        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile file, string Text, string Title)
        {

            var path = "/images/" + file.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }


            Image image = new Image() { ImagePath = path };

            imageManager.Insert(image);
            var imageId = imageManager.Get().Where(e => e.ImagePath == image.ImagePath).FirstOrDefault().Id;
            Projects projects = new Projects()
            {
                Text = Text,
                Title = Title,
                Image_Id = imageId
            };

            projectsManager.Insert(projects);

            var allProjects = projectsManager.GetAll().ToList();
            var images = imageManager.GetAll().ToList();
            return View("Projects", new ProjectsViewModel()
            {
                ProjectsLst = allProjects,
                ImageLst = images
            });

        }
        //public void AddArticle()
        //{


        //}

        //public void DeleteOrRecoverArticle(NewsDTO newsDTO)
        //{
        //    newsManager.DeleteOrRecover(newsDTO.Id);
        //}

        //public void UpdateArticle(NewsDTO newsDTO)
        //{
        //    newsManager.Update(newsDTO);
        //}
    }
}