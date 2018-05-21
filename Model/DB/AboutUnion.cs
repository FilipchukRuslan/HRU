using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class AboutUnion
    {
        public int Id { get; set; }
        public string Union { get; set; }
        public string Mission { get; set; }
        public string Goals { get; set; }

        public int Partners_Id { get; set; }
        public virtual Partners Partners { get; set; }

        public int Image_Id { get; set; }
        public virtual Image Image { get; set; }
    }
}
