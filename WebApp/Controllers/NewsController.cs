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
    public class NewsController : Controller
    {
        IHostingEnvironment appEnvironment;
        private readonly ICarouselManager carouselManager;
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        private readonly IFaceBookManager faceBookManager;
        private readonly IProjectsManager projectsManager;
        private readonly IVideoManager videoManager;
        private readonly IPartnersManager partnersManager;

        public NewsController(ICarouselManager carouselManager,
            INewsManager newsManager,
            IImageManager imageManager,
            IFaceBookManager faceBookManager,
            IVideoManager videoManager,
            IProjectsManager projectsManager,
            IHostingEnvironment appEnvironment,
            IPartnersManager partnersManager)
        {
            this.carouselManager = carouselManager;
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.faceBookManager = faceBookManager;
            this.projectsManager = projectsManager;
            this.videoManager = videoManager;
            this.appEnvironment = appEnvironment;
            this.partnersManager = partnersManager;
        }

        public IActionResult News(int page = 1)
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
            while (projectsManager.GetAll().Count() < 4)
            {
                projectsManager.Insert(new Projects() { Image_Id = 1, Title = "Default text" });
            }
            if (!faceBookManager.GetAll().Any())
            {
                faceBookManager.Insert(new FaceBook() { FBPost = "Default text", Date = DateTime.Now, PersonLink = "#", PersonName = "Default Name" });
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
            var projLst = projectsManager.GetAll().ToList();
            var videoLst = videoManager.GetAll().ToList();
            var imgLst = imageManager.GetAll().ToList();
            var partnersLst = partnersManager.GetAll().ToList();
            int pageSize = 4;
            
            News [] news = newsManager.GetAll().ToArray();


            for (int i = 0; i < newsManager.GetAll().Count(); i++)//construction to sort dates
            {
                for (int i2 = i+1; i2 < newsManager.GetAll().Count(); i2++)
                {
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();
                    News temp = null;
                    foreach (MonthEnum item in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (news[i].Month == item.ToString())
                        {
                            date1 = new DateTime(news[i].Year, (int)item+1, news[i].Day);
                            break;
                        }
                    }

                    foreach (MonthEnum item2 in Enum.GetValues(typeof(MonthEnum)))
                    {
                        if (news[i2].Month == item2.ToString())
                        {
                            date2 = new DateTime(news[i2].Year, (int)item2+1, news[i2].Day);
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
            var count = news.Count();
            var items = news.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel NewsPageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                NewsPageInfo = NewsPageViewModel,
                News = items
            };
            return View(new StartPageViewModel()
            {
                CarouselLst = carouselLst,
                FaceBookLst = fbLst,
                ProjectsLst = projLst,
                VideoLst = videoLst,
                NewsLst = news.ToList(),
                ImagesLst = imgLst,
                PartnersLst = partnersLst,
                IndexViewModel = viewModel
            });

        }
        
        public IActionResult ShowNews(int id)
        {
            var article = newsManager.Get().Where(e => e.Id == id).FirstOrDefault();
            var image = imageManager.Get().Where(e => e.Id == article.Image_Id).FirstOrDefault();
            return View(new ShowNewsViewModel() {
                Article = article,
                Image = image
            });
        }
        public IActionResult DeleteNews(int id)
        {
            var news = newsManager.Get().Where(e=>e.Id==id).FirstOrDefault();
            newsManager.Delete(news);
            return RedirectToAction("News");
        }
    }
}