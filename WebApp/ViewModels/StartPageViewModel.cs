using Model.DB;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class StartPageViewModel
    {

        public int id { get; set; }
        public List<News> NewsLst { get; set; }
        public List<Video> VideoLst { get; set; }
        public List<FaceBook> FaceBookLst { get; set; }
        public List<Projects> ProjectsLst { get; set; }
        public List<Carousel> CarouselLst { get; set; }
        public List<Image> ImagesLst { get; set; }
        public List<Partners> PartnersLst { get; set; }
        public List<AboutUnion> AboutUnionLst { get; set; }
        public List<Media> MediaLst { get; set; }
        public AbstractInfo Info { get; set; }
        public IndexViewModel IndexViewModel { get; set; }

    }
}
