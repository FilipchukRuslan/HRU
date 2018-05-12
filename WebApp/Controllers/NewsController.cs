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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsManager newsManager;
        private readonly IImageManager imageManager;
        IHostingEnvironment appEnvironment;

        public NewsController(INewsManager newsManager, IImageManager imageManager, IHostingEnvironment appEnvironment)
        {
            this.newsManager = newsManager;
            this.imageManager = imageManager;
            this.appEnvironment = appEnvironment;
        }
        
        public IActionResult News()
        {
            try
            {
                var news = newsManager.GetAll().ToList();
                var images = imageManager.GetAll().ToList();

                return View(new NewsViewModel()
                {
                    NewsLst = news,
                    ImageLst = images
                });
            }
            catch (Exception ex)
            {

            }
            return View();

        }
        [HttpPost("UploadF")]
        public async Task<IActionResult> Post(IFormFile file, string Text, string Title)
        {

            var path = "/images/" + file.FileName;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            

            Image image = new Image() { ImagePath = path };

            imageManager.Insert(image);
            var imageId = imageManager.Get().Where(e => e.ImagePath == image.ImagePath).FirstOrDefault().Id;
            News news = new News() { Text = Text, Title = Title,
                Image_Id = imageId,
                Day = DateTime.Today.Day,
                Month = Enum.GetName(typeof(MonthEnum), DateTime.Today.Month - 1),
                Year = DateTime.Today.Year };

            newsManager.Insert(news);

            var allNews = newsManager.GetAll().ToList();
            var images = imageManager.GetAll().ToList();
            return View("News", new NewsViewModel()
            {
                NewsLst = allNews,
                ImageLst = images
            });

        }
        //public void AddArticle()
        //{


        //}

        //public void DeleteOrRecoverArticle(NewsDTO newsDTO)
        //{
        //    newsManager.DeleteOrRecover(newsDTO.Id);
        //}

        //public void UpdateArticle(NewsDTO newsDTO)
        //{
        //    newsManager.Update(newsDTO);
        //}
    }
}