﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Projects
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        
        public int Image_Id { get; set; }
        public virtual Image Image { get; set; }
    }
}
