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
        readonly IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPartnersManager partnersManager;
        private readonly IMediaManager mediaManager;
        private readonly IAbstractInfoManager abstractInfoManager;

        public HomeController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IPartnersManager partnersManager,
            IMediaManager mediaManager,
            IAbstractInfoManager abstractInfoManager)
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
            this.abstractInfoManager = abstractInfoManager;
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
                    Month = ((MonthEnum)DateTime.Today.Month - 1).ToString(),
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
            
            News[] news = newsManager.GetAll().ToArray();


            for (int i = 0; i < newsManager.GetAll().Count(); i++)//construction to sort dates
            {
                for (int i2 = i + 1; i2 < newsManager.GetAll().Count(); i2++)
                {
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();
                    News temp = null;
                    foreach (MonthEnum item in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (news[i].Month == item.ToString())
                        {
                            date1 = new DateTime(news[i].Year, (int)item + 1, news[i].Day);
                            break;
                        }
                    }

                    foreach (MonthEnum item2 in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (news[i2].Month == item2.ToString())
                        {
                            date2 = new DateTime(news[i2].Year, (int)item2 + 1, news[i2].Day);
                            break;
                        }
                    }

                    if (DateTime.Compare(date1, date2) < 0)
                    {
                        temp = news[i];
                        news[i] = news[i2];
                        news[i2] = temp;
                    }
                    else
                    {
                        break;
                    }
                }

            }


            List<News> newsLst;
            if (newsManager.GetAll().Count() > 8)
            {
                newsLst = news.Take(8).ToList();
            }
            else
            {
                newsLst = news.ToList();
            }
            
            var projLst = projectsManager.GetAll().ToList();
            var partnersLst = partnersManager.GetAll().ToList();
            var videoLst = videoManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();
            var mediaLst = mediaManager.GetAll().ToList();
            AbstractInfo info = null;
            if (abstractInfoManager.GetAll().Count() < 1)
            {
                info = new AbstractInfo() { Title = "по умолчанию", Text = "09826259810" };
            }
            else
            {
                info = abstractInfoManager.Get().LastOrDefault();
            }

            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = newsLst,
                ImagesLst = imgLst,
                PartnersLst = partnersLst,
                MediaLst = mediaLst,
                Info = info
            });
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
