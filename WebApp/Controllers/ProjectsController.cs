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

        public ProjectsController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            var projects = projectsManager.GetAll().Reverse().ToList();
            var count = projects.Count();
            var items = projects.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel ProjectsPageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                ProjectsPageInfo = ProjectsPageViewModel,
                Projects = items
            };
            
            while (projectsManager.GetAll().Count() < 3)
            {
                projectsManager.Insert(new Projects() { Image_Id = 1, Title = "Default text" });
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

            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = newsLst,
                ImagesLst = imgLst,
                IndexViewModel = viewModel
            });

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