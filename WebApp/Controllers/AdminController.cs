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
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]
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
        private readonly IMediaManager mediaManager;
        private readonly IAbstractInfoManager abstractInfoManager;

        public AdminController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IAboutUnionManager aboutUnionManager,
            IPartnersManager partnersManager, IMediaManager mediaManager, IAbstractInfoManager abstractInfoManager)
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
            this.mediaManager = mediaManager;
            this.abstractInfoManager = abstractInfoManager;
        }
        public IActionResult News(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var item = newsManager.GetById(id);
            return View(item);
        }
        public IActionResult Projects(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var item = projectsManager.GetById(id);
            return View(item);
        }
        public IActionResult AboutUnion(int id)
        {
            var carouselLst = carouselManager.GetAll().ToList();
            var fbLst = faceBookManager.GetAll().ToList();
            var projLst = projectsManager.GetAll().ToList();
            var newsLst = newsManager.GetAll().ToList();
            var videoLst = videoManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();
            var partnersManagerLst = partnersManager.GetAll().ToList();
            var aboutUnionLst = aboutUnionManager.GetAll().ToList();

            
            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = newsLst,
                ImagesLst = imgLst,
                PartnersLst = partnersManagerLst,
                AboutUnionLst = aboutUnionLst,
                id = id
            });


        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult FaceBook()
        {
            return View();
        }
        public IActionResult Carousel(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var item = carouselManager.GetById(id);
            return View(item);
        }
        public IActionResult Media()
        {
            return View();
        }
        public IActionResult Contacts()
        {
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
                Info = info
            });
        }




        public IActionResult EditContacts(string Text)
        {
            abstractInfoManager.Insert(new AbstractInfo() { Text = Text });
            return RedirectToAction("Contacts");
        }

        [HttpPost("UploadVideo")]
        public IActionResult PostVideo(string Video, string Text, DateTime custom_date)
        {
            videoManager.Insert(new Video()
            {
                Text = Text,
                VideoFile = Video,
                Day = custom_date.Day,
                Month = Enum.GetName(typeof(MonthEnum), custom_date.Month - 1),
                Year = custom_date.Year
            });
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
                Day = formModelClass.dat.Day,
                Month = Enum.GetName(typeof(MonthEnum), formModelClass.dat.Month - 1),
                Year = formModelClass.dat.Year
            };
            newsManager.Insert(news);
            return RedirectToAction("News");

        }

        [HttpPost("UploadTeamMember")]
        public async Task<IActionResult> PostTeamMember(IFormFile uploads, string Text, int id)
        {
            if (id != 0)
            {
                var item = partnersManager.Get().Where(e => e.Id == id).FirstOrDefault();
                item.ParnerAbout = Text;
                string path1 = "/images/" + uploads.FileName;
                if (path1 == null)
                    path1 = "";

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
                {
                    await uploads.CopyToAsync(fileStream);
                }
                Image image = new Image() { ImagePath = path1 };

                imageManager.Insert(image);
                item.Image_Id = image.Id;
                partnersManager.Update(item);
            }
            else
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
                    isCrew = false,
                    ParnerAbout = Text
                });
            }
            return RedirectToAction("AboutUnion");
        }

        [HttpPost("UploadMedia")]
        public IActionResult PostMedia(string Text, string Ref, string Name, DateTime dat)
        {
            Media media = new Media()
            {
                MediaName = Ref,
                Text = Text,
                Name = Name,
                Day = dat.Day,
                Month = Enum.GetName(typeof(MonthEnum), dat.Month - 1),
                Year = dat.Year
            };

            mediaManager.Insert(media);
            return RedirectToAction("Media");
        }

        [HttpPost("UploadCrew")]
        public async Task<IActionResult> PostCrew(IFormFile uploads, string Text, int id)
        {
            if (id != 0)
            {
                var item = partnersManager.Get().Where(e => e.Id == id).FirstOrDefault();

                item.ParnerAbout = Text;
                string path1 = "/images/" + uploads.FileName;
                if (path1 == null)
                    path1 = "";

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
                {
                    await uploads.CopyToAsync(fileStream);
                }
                Image image = new Image() { ImagePath = path1 };

                imageManager.Insert(image);
                item.Image_Id = image.Id;

                partnersManager.Update(item);
            }
            else
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
                    isCrew = true,
                    ParnerAbout = Text
                });
            }
            return RedirectToAction("AboutUnion");
        }

        public IActionResult Info(string mce, string mission, string goals)
        {
            if (!aboutUnionManager.GetAll().Any())
            {
                aboutUnionManager.Insert(new AboutUnion()
                {
                    Mission = mission,
                    Union = mce,
                    Goals = goals
                });
                return RedirectToAction("AboutUnion");
            }
            aboutUnionManager.Update(new AboutUnion()
            {
                Mission = mission,
                Union = mce,
                Goals = goals
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