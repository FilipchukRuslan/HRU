using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoFile { get; set; }
        public string Text { get; set; }
        public int Day { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}
