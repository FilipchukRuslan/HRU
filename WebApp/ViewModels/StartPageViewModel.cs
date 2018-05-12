using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class StartPageViewModel
    {
        public List<News> NewsLst { get; set; }
        public List<Video> VideoLst { get; set; }
        public List<FaceBook> FaceBookLst { get; set; }
        public List<Projects> ProjectsLst { get; set; }
        public List<Carousel> CarouselLst { get; set; }
        public List<Image> ImagesLst { get; set; }
        public List<Person> PersonsLst { get; set; }
    }
}
