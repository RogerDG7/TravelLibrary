using System;
namespace TravelLibrary.Models.Response
{
    public class BookResponseModel
    {
        public long Isbn { get; set; }
        public string EditorialName { get; set; }
        public string Titlle { get; set; }
        public string Sypnosis { get; set; }
        public string NumPages { get; set; }
        public string AuthorInfo { get; set; }
    }
}
