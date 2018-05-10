using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class NewsViewModel
    {
        public List<NewsDTO> NewsLst { get; set; }
        public List<ImageDTO> ImageLst { get; set; }
    }
}
