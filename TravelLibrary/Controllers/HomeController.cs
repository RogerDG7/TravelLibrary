using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelLibrary.Data;
using TravelLibrary.Helpers;
using TravelLibrary.Models;
using TravelLibrary.Models.Entities;
using TravelLibrary.Models.Response;
using TravelLibrary.Services;
using TravelLibrary.Services.TokenService;

namespace TravelLibrary.Controllers
{
    [TypeFilter(typeof(ExceptionManagerFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TravelLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, TravelLibraryContext context,
                              IMapper mapper, ITokenService tokenService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Privacy"));
            }
            GetBooksService bookService = new(_context);
            List<Book> books = await bookService.GetBooks();
            return View(_mapper.Map<List<BookResponseModel>>(books));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(string nameBook)
        {
            List<Book> books = new();
            GetBooksService bookService = new(_context);
            if (string.IsNullOrEmpty(nameBook))
            {
                books = await bookService.GetBooks();
            }
            else
            {
                books = await bookService.GetBooksLikeName(nameBook);
            }
            return View(_mapper.Map<List<BookResponseModel>>(books));
        }

        [Authorize]
        public IActionResult GetInfo(long id)
        {
            return RedirectToAction("Index","Books", new { isbn = id});
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GetToken()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                string generatedToken = _tokenService.BuildTokenGuest();
                HttpContext.Session.SetString("Token", generatedToken);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
