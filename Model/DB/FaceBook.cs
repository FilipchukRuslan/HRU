﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class FaceBook
    {
        public int Id { get; set; }
        public string FBPost { get; set; }

        public int Person_Id { get; set; }
        public Person Person { get; set; }
    }
}
