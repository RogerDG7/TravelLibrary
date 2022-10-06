using System;
using System.Collections.Generic;

namespace TravelLibrary.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        //Keys
        public virtual ICollection<AuthorHasBook> AuthorHasBooks { get; set; }
    }
}
