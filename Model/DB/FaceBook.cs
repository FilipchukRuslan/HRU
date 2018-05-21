using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class FaceBook
    {
        public int Id { get; set; }
        public string FBPost { get; set; }
        public DateTime Date { get; set; }
        
        public string PersonName { get; set; }
        public string PersonLink { get; set; }

        public int Image_Id { get; set; }
        public virtual Image Image { get; set; }
    }
}
