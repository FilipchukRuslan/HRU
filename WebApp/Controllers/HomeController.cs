using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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

        [HttpPost("UploadCarousel")]
        public async Task<IActionResult> PostCarousel(IFormFileCollection uploads, string Text, int num)
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

            imageManager.Insert(image);

            var imageId = imageManager.Get().Where(e => e.ImagePath == image.ImagePath).FirstOrDefault().Id;

            try
            {
                Carousel entity = carouselManager.Get().Where(e => e.Id == num).FirstOrDefault();
                entity.Image_Id = imageId;
                entity.Text = Text;
                entity.ImageMin = path2;
                carouselManager.Update(entity);
            }
            catch (Exception ex)
            {
                Carousel carousel = new Carousel()
                {
                    Image_Id = imageId,
                    Text = Text
                };
                carouselManager.Insert(carousel);
            }

            return RedirectToAction("Index");
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
