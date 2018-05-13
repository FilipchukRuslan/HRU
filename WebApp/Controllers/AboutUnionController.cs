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
        private readonly IPersonManager personsManager;

        public AboutUnionController(ICarouselManager carouselManager,
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

        [HttpPost("UploadTeamMember")]
        public async Task<IActionResult> PostTeamMember(IFormFile uploads, string MemberName, string Ref)
        {
            string path1 = "/images/" + uploads.FileName;
            if (path1 == null)
                path1 = "";

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path1, FileMode.Create))
            {
                await uploads.CopyToAsync(fileStream);
            }
            
            Person person = new Person() { ProfilePhoto = path1, Name = MemberName , ReferenceFB = Ref };

            personsManager.Insert(person);
            
            return RedirectToAction("Index");
        }
    }
}