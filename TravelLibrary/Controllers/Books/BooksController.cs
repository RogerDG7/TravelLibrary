using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelLibrary.Data;
using TravelLibrary.Helpers;
using TravelLibrary.Models.Entities;
using TravelLibrary.Models.Response;
using TravelLibrary.Services;

namespace TravelLibrary.Controllers.Books
{
    [TypeFilter(typeof(ExceptionManagerFilter))]
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TravelLibraryContext _context;
        private readonly IMapper _mapper;

        public BooksController(ILogger<HomeController> logger, TravelLibraryContext context,
                               IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index(long isbn)
        {
            GetBooksService bookService = new(_context);
            Book book = await bookService.GetBookByIsbn(isbn);
            return View(_mapper.Map<BookResponseModel>(book));
        }
    }
}