using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using Common;
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

        public NewsController(INewsManager newsManager, IImageManager imageManager)
        {
            this.newsManager = newsManager;
            this.imageManager = imageManager;
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
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files, string Text, string Title)
        {
            long size = files.Sum(f => f.Length);
            var name = files.Select(e => e.FileName);

            var filePath = Path.GetTempFileName(); ;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            Image image = new Image() { ImagePath = filePath+name};

            imageManager.Insert(image);
            var imageId = imageManager.Get().Where(e => e.ImagePath == image.ImagePath).FirstOrDefault().Id;
            News newsDTO = new News() { Text = Text, Title = Title,
                Image_Id = imageId,
                Day = DateTime.Today.Day,
                Month = Enum.GetName(typeof(MonthEnum), DateTime.Today.Month - 1),
                Year = DateTime.Today.Year };

            newsManager.Insert(newsDTO);

            var news = newsManager.GetAll().ToList();
            var images = imageManager.GetAll().ToList();
            return View("News", new NewsViewModel()
            {
                NewsLst = news,
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