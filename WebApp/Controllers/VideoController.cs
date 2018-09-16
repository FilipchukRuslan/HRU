using BAL.Interfaces;
using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VideoController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;

        public VideoController(ICarouselManager carouselManager,
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
            if (!videoManager.GetAll().Any())
            {
                videoManager.Insert(new Video() { Text = "Default Text", VideoFile = "<iframe width=\"854\" height=\"480\" src=\"https://www.youtube.com/embed/TFHcJMzgYiE\" frameborder=\"0\" allow=\"autoplay; encrypted-media\" allowfullscreen></iframe>" });
            }
            var carouselLst = carouselManager.GetAll().ToList();
            var fbLst = faceBookManager.GetAll().ToList();
            var newsLst = newsManager.GetAll().ToList();
            var projLst = projectsManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();

            ///////////////////////////////////////////////
            Video[] videoLst = videoManager.GetAll().ToArray();


            for (int i = 0; i < videoManager.GetAll().Count(); i++)//construction to sort dates
            {
                for (int i2 = i + 1; i2 < videoManager.GetAll().Count(); i2++)
                {
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();
                    Video temp = null;
                    foreach (MonthEnum item in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (videoLst[i].Month == item.ToString())
                        {
                            date1 = new DateTime(videoLst[i].Year, (int)item + 1, videoLst[i].Day);
                            break;
                        }
                    }

                    foreach (MonthEnum item2 in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (videoLst[i2].Month == item2.ToString())
                        {
                            date2 = new DateTime(videoLst[i2].Year, (int)item2 + 1, videoLst[i2].Day);
                            break;
                        }
                    }

                    if (DateTime.Compare(date1, date2) < 0)
                    {
                        temp = videoLst[i];
                        videoLst[i] = videoLst[i2];
                        videoLst[i2] = temp;
                    }
                    else
                    {
                        break;
                    }
                }

            }


            int pageSize = 3;   // количество элементов на странице

            //var videos = videoManager.GetAll().Reverse().ToList();
            var count = videoLst.Count();
            var items = videoLst.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel VideosPageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                VideosPageInfo = VideosPageViewModel,
                Videos = items
            };
            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst.ToList(),
                NewsLst = newsLst,
                ImagesLst = imgLst,
                IndexViewModel = viewModel
            });


        }
        public IActionResult DeleteVideo(int id)
        {
            var video = videoManager.Get().Where(e => e.Id == id).FirstOrDefault();
            videoManager.Delete(video);
            return RedirectToAction("Index");
        }
    }
}
