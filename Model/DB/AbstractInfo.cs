using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class AbstractInfo
    {
        public int Id { get; set; }
        public string Text { get; set; }//if need to add contacts information or smth else

        public string Title { get; set; }//if need to add name of organization or smth else
        
        public int Image_Id { get; set; }//if need to add logo of organization or smth else
        public virtual Image Image { get; set; }
    }
}
