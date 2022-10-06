using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelLibrary.Data;
using TravelLibrary.Models.Entities;

namespace TravelLibrary.Services
{
    public class GetBooksService
    {

        private readonly TravelLibraryContext _context;

        public GetBooksService(TravelLibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooks()
        {
            List<Book> books = await _context.Books.OrderBy(x => x.Isbn).ThenBy(x => x.Titlle)
                                             .Include(x => x.Editorials)
                                             .Include(x => x.AuthorHasBooks).ThenInclude(x => x.Authors)
                                             .ToListAsync();
            return books;
        }

        public async Task<Book> GetBookByIsbn(long isbn)
        {
            Book book = await _context.Books.Where(x => x.Isbn == isbn)
                                            .Include(x => x.Editorials)
                                            .Include(x => x.AuthorHasBooks).ThenInclude(x => x.Authors)
                                            .FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<Book>> GetBooksLikeName(string nameBook)
        {
            List<Book> books = await _context.Books.Where(x => x.Titlle.ToUpper().Contains(nameBook.ToUpper()))
                                          .OrderBy(x => x.Isbn).ThenBy(x => x.Titlle)
                                          .Include(x => x.Editorials)
                                          .Include(x => x.AuthorHasBooks).ThenInclude(x => x.Authors)
                                          .ToListAsync();
            return books;
        }


    }
}
