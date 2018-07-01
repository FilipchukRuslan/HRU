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
    public class MediaController : Controller
    {
        private readonly IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPartnersManager partnersManager;
        private readonly IMediaManager mediaManager;

        public MediaController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IPartnersManager partnersManager,
            IMediaManager mediaManager)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
            this.partnersManager = partnersManager;
            this.mediaManager = mediaManager;
        }
        public IActionResult Index()
        {
            if (!imageManager.GetAll().Any())
            {
                imageManager.Insert(new Image() { ImagePath = "http://lavenderhillhigh.co.za/wp-content/gallery/fundraising/default-image.jpg" });
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

            if (!faceBookManager.GetAll().Any())
            {
                faceBookManager.Insert(new FaceBook() { FBPost = "Default text", Date = DateTime.Now, Image_Id = 1, PersonLink = "#", PersonName = "Default Name" });
            }
            while (videoManager.GetAll().Count() < 4)
            {
                videoManager.Insert(new Video()
                {
                    Text = "Default Text",
                    VideoFile = "<iframe width=\"854\" height=\"480\" src=\"https://www.youtube.com/embed/TFHcJMzgYiE\" frameborder=\"0\" allow=\"autoplay; encrypted-media\" allowfullscreen></iframe>",
                    Day = DateTime.Today.Day,
                    Month = Enum.GetName(typeof(MonthEnum), DateTime.Today.Month - 1),
                    Year = DateTime.Today.Year
                });
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
            var partnersLst = partnersManager.GetAll().ToList();
            var videoLst = videoManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();
            var mediaLst = mediaManager.GetAll().ToList();

            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = newsLst,
                ImagesLst = imgLst,
                PartnersLst = partnersLst,
                MediaLst = mediaLst
            });
        }
    }
}