using System;
using System.Collections.Generic;

namespace TravelLibrary.Models.Entities
{
    public class Editorial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }

        //key
        public virtual ICollection<Book> Books { get; set; }
    }
}
