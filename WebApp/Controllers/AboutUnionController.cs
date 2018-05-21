using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AboutUnionController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPartnersManager partnersManager;
        private readonly IAboutUnionManager aboutUnionManager;

        public AboutUnionController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IPartnersManager partnersManager,
            IAboutUnionManager aboutUnionManager)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
            this.partnersManager = partnersManager;
            this.aboutUnionManager = aboutUnionManager;
        }

        public IActionResult Index()
        {
            var carouselLst = carouselManager.GetAll().ToList();
            var fbLst = faceBookManager.GetAll().ToList();
            var newsLst = newsManager.GetAll().ToList();
            var projLst = projectsManager.GetAll().ToList();
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
                AboutUnionLst = aboutUnionLst
            });
            
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

            partnersManager.Insert(new Partners() {
                Image_Id = image.Id,
                ParnerName = Name,
                ParnerAbout = Text
            });
            
            return RedirectToAction("Index");
        }

        [HttpPost("UploadInfo")]
        public IActionResult Info(string HRU, string Mission, string Goals)
        {
            aboutUnionManager.Insert(new AboutUnion() {
                Mission = Mission,
                Union = HRU,
                Goals = Goals
            });
            return RedirectToAction("Index");
        }
    }
}