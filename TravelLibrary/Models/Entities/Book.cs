using System;
using System.Collections.Generic;

namespace TravelLibrary.Models.Entities
{
    public class Book
    {
        public long Isbn { get; set; }
        public int EditorialId { get; set; }
        public string Titlle { get; set; }
        public string Sypnosis { get; set; }
        public string NumPages { get; set; }

        //Keys
        public virtual ICollection<AuthorHasBook> AuthorHasBooks { get; set; }
        public virtual Editorial Editorials { get; set; }
    }
}
