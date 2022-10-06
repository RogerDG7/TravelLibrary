using System;
using System.Linq;
using AutoMapper;
using TravelLibrary.Models.Entities;
using TravelLibrary.Models.Response;

namespace TravelLibrary.Mapper
{
    public class BookProfileMap : Profile
    {
        public BookProfileMap()
        {
            CreateMap<Book, BookResponseModel>()
                .ForMember(ent => ent.EditorialName, opt => opt.MapFrom(src => src.Editorials.Name))
                .ForMember(ent => ent.AuthorInfo, opt =>
                    opt.MapFrom(src => src.AuthorHasBooks.AsQueryable().Where(x => x.BookIsbn == src.Isbn)
                                        .Select(x => x.Authors.Name + " " + x.Authors.LastName)
                                        .FirstOrDefault().ToString()))
                .ReverseMap();
        }
    }
}
