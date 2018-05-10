using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Contacts
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
