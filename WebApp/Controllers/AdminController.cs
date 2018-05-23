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
    public class AdminController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IAboutUnionManager aboutUnionManager;
        private readonly IPartnersManager partnersManager;

        public AdminController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IAboutUnionManager aboutUnionManager,
            IPartnersManager partnersManager)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
            this.aboutUnionManager = aboutUnionManager;
            this.partnersManager = partnersManager;
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }
        public IActionResult AboutUnion()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult FaceBook()
        {
            return View();
        }
        public IActionResult Carousel()
        {
            return View();
        }
        public IActionResult Media()
        {
            return View();
        }


        [HttpPost("UploadVideo")]
        public IActionResult PostVideo(string Video, string Text)
        {
            videoManager.Insert(new Video() { Text = Text, VideoFile = Video });
            return RedirectToAction("Video");
        }

        [HttpPost("UploadProjects")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var path = "/images/" + file.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            Image image = new Image() { ImagePath = path };

            imageManager.Insert(image);
            return RedirectToAction("Projects");
        }

        [HttpPost("UploadProjectsText")]
        public IActionResult PostText(string Text, string Title)
        {
            var lastImages = imageManager.Get().Reverse().Take(1).FirstOrDefault();
            Projects projects = new Projects()
            {
                Text = Text,
                Title = Title,
                Image_Id = lastImages.Id
            };

            projectsManager.Insert(projects);

            return RedirectToAction("Projects");
        }



        [HttpPost("UploadF")]
        public async Task<IActionResult> PostImage(IFormFile file)
        {
            var path = "/images/" + file.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }


            Image image = new Image() { ImagePath = path };

            imageManager.Insert(image);
            return RedirectToAction("News");
        }

        public IActionResult PostForm(FormModelClass formModelClass)
        {
            var imageId = imageManager.Get().LastOrDefault().Id;
            News news = new News()
            {
                Text = formModelClass.text,
                Title = formModelClass.title,
                Image_Id = imageId,
                Day = DateTime.Today.Day,
                Month = Enum.GetName(typeof(MonthEnum), DateTime.Today.Month - 1),
                Year = DateTime.Today.Year
            };

            newsManager.Insert(news);
            return RedirectToAction("News");

        }

        [HttpPost("UploadTeamMember")]
        public async Task<IActionResult> PostTeamMember(IFormFile uploads, string Text, string Name)
        {
            string path1 = "/images/" + uploads.FileName;
            if (path1 == null)
                path1 = "";

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
            {
                await uploads.CopyToAsync(fileStream);
            }
            Image image = new Image() { ImagePath = path1 };

            imageManager.Insert(image);

            partnersManager.Insert(new Partners()
            {
                Image_Id = image.Id,
                ParnerName = Name,
                ParnerAbout = Text
            });

            return RedirectToAction("AboutUnion");
        }

        [HttpPost("UploadInfo")]
        public IActionResult Info(string HRU, string Mission, string Goals)
        {
            aboutUnionManager.Insert(new AboutUnion()
            {
                Mission = Mission,
                Union = HRU,
                Goals = Goals
            });
            return RedirectToAction("AboutUnion");
        }

        [HttpPost("UploadFB")]
        public async Task<IActionResult> PostFB(IFormFile uploads, string Text, string Person, string PersonLink)
        {
            string path1 = "/images/" + uploads.FileName;
            if (path1 == null)
                path1 = "";

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
            {
                await uploads.CopyToAsync(fileStream);
            }
            Image image = new Image() { ImagePath = path1 };
            imageManager.Insert(image);
            var fb = new FaceBook()
            {
                FBPost = Text,
                PersonName = Person,
                PersonLink = PersonLink,
                Image_Id = image.Id,
                Date = DateTime.Now
            };

            faceBookManager.Insert(fb);

            return RedirectToAction("FaceBook");
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

            return RedirectToAction("Carousel");
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

            return RedirectToAction("Carousel");
        }

    }
}