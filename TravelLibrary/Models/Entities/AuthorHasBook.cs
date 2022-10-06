using System;
namespace TravelLibrary.Models.Entities
{
    public class AuthorHasBook
    {
        public int AuthorID { get; set; }
        public long BookIsbn { get; set; }

        //keys
        public virtual Author Authors { get; set; }
        public virtual Book Books { get; set; }
    }
}
