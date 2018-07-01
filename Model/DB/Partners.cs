using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Partners
    {
        public int Id { get; set; }
        public string ParnerName { get; set; }
        public string ParnerAbout { get; set; }
        public bool isCrew { get; set; }

        public int Image_Id { get; set; }
        public virtual Image Image { get; set; }

        public int AboutUnion_Id { get; set; }
        public virtual AboutUnion AboutUnion { get; set; }
    }
}
