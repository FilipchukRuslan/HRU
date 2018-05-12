using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ProjectsViewModel
    {
        public List<Projects> ProjectsLst { get; set; }
        public List<Image> ImageLst { get; set; }
    }
}
