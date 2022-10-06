using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TravelLibrary.Controllers;
using TravelLibrary.Data;
using TravelLibrary.Mapper;
using TravelLibrary.Models.Entities;
using TravelLibrary.Services;

namespace TravelLibraryTest
{
    [TestFixture]
    public class Tests
    {
        private GetBooksService bookService;

        [SetUp]
        public void Setup()
        {
            bookService = new(new TravelLibraryContext());
        }

        [Test]
        public async Task Get_list_all_books()
        {
            List<Book> books = await bookService.GetBooks();
            Assert.IsTrue(books != null && books.Count > 0);
        }

        [Test]
        public async Task Get_book_by_isbn()
        {
            Book book = await bookService.GetBookByIsbn(8420685623);
            Assert.IsNotNull(book);
        }

        [Test]
        public async Task Get_book_by_isbn_not_result()
        {
            Book book = await bookService.GetBookByIsbn(23455);
            Assert.IsNull(book);
        }

        [Test]
        public async Task Get_book_like_name()
        {
            List<Book> books = await bookService.GetBooksLikeName("Historia");
            Assert.IsTrue(books != null && books.Count > 0);
        }

        [Test]
        public async Task Get_book_like_name_blank_parameter()
        {
            //no deberia ocurrir ya que esta controlado para que nunca llegue vacio
            //se prueba igual que no reviente si el parametro es blanco

            List<Book> books = await bookService.GetBooksLikeName("");
            Assert.IsNotNull(books);
        }
    }
}
