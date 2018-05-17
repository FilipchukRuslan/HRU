using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Projects> Projects { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public PageViewModel NewsPageInfo { get; set; }
        public PageViewModel ProjectsPageInfo { get; set; }
        public PageViewModel VideosPageInfo { get; set; }
    }
}
