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

        public IActionResult Index()
        {
            if (!carouselManager.GetAll().Any())
            {
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = " ", Image_Id = 1 });
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = " ", Image_Id = 2 });
                carouselManager.Insert(new Carousel() { ImageMin = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg", Text = " ", Image_Id = 3 });
            }
            if (!imageManager.GetAll().Any())
            {
                imageManager.Insert(new Image() { ImagePath = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg" });
                imageManager.Insert(new Image() { ImagePath = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg" });
                imageManager.Insert(new Image() { ImagePath = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg" });
            }
            var carouselLst = carouselManager.GetAll().ToList();
            var fbLst = faceBookManager.GetAll().ToList();
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
                PersonsLst = personsLst
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