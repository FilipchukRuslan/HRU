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
using Model.Entity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPersonManager personsManager;

        public ProjectsController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IPersonManager personsManager)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
            this.personsManager = personsManager;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 9;   // количество элементов на странице

            var projects = projectsManager.GetAll().Reverse().ToList();
            var count = projects.Count();
            var items = projects.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel ProjectsPageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                ProjectsPageInfo = ProjectsPageViewModel,
                Projects = items
            };
            if (!imageManager.GetAll().Any())
            {
                imageManager.Insert(new Image() { ImagePath = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg" });
            }
            if (!carouselManager.GetAll().Any())
            {
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = "Default text", Image_Id = 1 });
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = "Default text", Image_Id = 1 });
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = "Default text", Image_Id = 1 });
            }
            while (projectsManager.GetAll().Count() < 3)
            {
                projectsManager.Insert(new Projects() { Image_Id = 1, Title = "Default text" });
            }
            if (!personsManager.GetAll().Any())
            {
                personsManager.Insert(new Person() { Name = "Default Name", ProfilePhoto = "http://www.brilliant-stay.com/wp-content/uploads/2016/02/default-avatar_0.png", ReferenceFB = "#" });
            }
            if (!faceBookManager.GetAll().Any())
            {
                faceBookManager.Insert(new FaceBook() { FBPost = "Default text", Date = DateTime.Now, Person_Id = 1 });
            }
            while (videoManager.GetAll().Count() < 4)
            {
                videoManager.Insert(new Video() { Text = "Default Text", VideoFile = "<iframe width=\"854\" height=\"480\" src=\"https://www.youtube.com/embed/TFHcJMzgYiE\" frameborder=\"0\" allow=\"autoplay; encrypted-media\" allowfullscreen></iframe>" });
            }

            List<FaceBook> fbLst;
            if (faceBookManager.GetAll().Count() > 5)
            {
                fbLst = faceBookManager.Get().Reverse().Take(5).ToList();
            }
            else
            {
                fbLst = faceBookManager.GetAll().ToList();
            }

            var carouselLst = carouselManager.GetAll().ToList();
            var newsLst = newsManager.GetAll().ToList();
            var projLst = projectsManager.GetAll().ToList();
            var videoLst = videoManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();
            var personsLst = personsManager.GetAll().ToList();

            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = newsLst,
                ImagesLst = imgLst,
                PersonsLst = personsLst,
                IndexViewModel = viewModel
            });

        }

        [HttpPost("UploadProjects")]
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

            return RedirectToAction("Index");
        }

        public IActionResult ShowArtice(int id)
        {
            var projects = projectsManager.Get().Where(e => e.Id == id).FirstOrDefault();
            var image = imageManager.Get().Where(e => e.Id == projects.Image_Id).FirstOrDefault();
            return View(new ProjectsViewModel()
            {

                Image = image,
                Projects = projects
            });
        }
        public IActionResult DeleteProject(int id)
        {
            var proj = projectsManager.Get().Where(e => e.Id == id).FirstOrDefault();
            projectsManager.Delete(proj);
            return RedirectToAction("Index");
        }

    }
}