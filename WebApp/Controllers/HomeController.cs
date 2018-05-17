using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interfaces;
using BAL.Managers;
using Common;
using DAL;
using DAL.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.Entity;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPersonManager personsManager;

        public HomeController(ICarouselManager carouselManager,
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
                PersonsLst = personsLst
            });
        }

        [HttpPost("UploadFB")]
        public IActionResult PostFB(string Text, string Person)
        {
            var p = personsManager.Get().Where(e => e.Name == Person).FirstOrDefault();

            var fb = new FaceBook()
            {
                FBPost = Text,
                Person_Id = p.Id,
                Date = DateTime.Now
            };

            faceBookManager.Insert(fb);

            return RedirectToAction("Index");
        }

        public IActionResult PostFormCarousel(CarouselModel carouselModel)
        {
            var lastImages = imageManager.Get().Reverse().Take(2).ToList();

            try
            {
                Carousel entity = carouselManager.Get().Where(e => e.Id == carouselModel.num).FirstOrDefault();
                entity.Image_Id = lastImages[1].Id;
                entity.Title = carouselModel.title;
                entity.Text = carouselModel.text;
                entity.ImageMin = lastImages[0].ImagePath;
                carouselManager.Update(entity);
            }
            catch (Exception ex)
            {
                Carousel carousel = new Carousel()
                {
                    Image_Id = lastImages[1].Id,
                    Text = carouselModel.text
                };
                carouselManager.Insert(carousel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost("UploadCarousel")]
        public async Task<IActionResult> PostCarousel(IFormFileCollection uploads)
        {
            string path1 = "/images/" + uploads[0].FileName;
            if (path1 == null)
                path1 = "";

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
            {
                await uploads[0].CopyToAsync(fileStream);
            }

            string path2 = "/images/" + uploads[1].FileName;
            if (path2 == null)
                path2 = "";
            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path2, FileMode.Create))
            {
                await uploads[1].CopyToAsync(fileStream);
            }
            Image image = new Image() { ImagePath = path1 };
            Image image2 = new Image() { ImagePath = path2 };

            imageManager.Insert(image);
            imageManager.Insert(image2);

            return RedirectToAction("Index");
        }
        public IActionResult ShowCarousel(int id)
        {
            var slider = carouselManager.Get().Where(e => e.Id == id).FirstOrDefault();
            var image = imageManager.Get().Where(e => e.Id == slider.Image_Id).FirstOrDefault();
            return View(new CarouselViewModel()
            {
                Image = image,
                Carousel = slider
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DeleteFB(int id)
        {
            var fb = faceBookManager.Get().Where(e => e.Id == id).FirstOrDefault();
            faceBookManager.Delete(fb);
            return RedirectToAction("Index");
        }
        
        

    }
}
