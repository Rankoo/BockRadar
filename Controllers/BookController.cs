using BookRadar.Models;
using BookRadar.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRadar.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Book/Search
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        // POST: Book/Search
        [HttpPost]
        public async Task<IActionResult> Search(string author, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                ModelState.AddModelError(string.Empty, "Debes ingresar un autor.");
                return View();
            }

            var results = await _bookService.SearchByAuthorAsync(author, ct);
            return View("Results", results); // redirige a vista con los resultados
        }
    }
}
