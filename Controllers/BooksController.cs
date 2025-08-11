using BookRadar.Models;
using BookRadar.Repositories;
using BookRadar.Services;
using BookRadar.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRadar.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IHistorialRepository _historialRepository;

        public BooksController(IBookService bookService, IHistorialRepository historialRepository)
        {
            _bookService = bookService;
            _historialRepository = historialRepository;
        }

        // Página de búsqueda
        [HttpGet]
        public IActionResult Index()
        {
            return View(new BookSearchViewModel());
        }

        // Procesa la búsqueda y muestra resultados
        [HttpPost]
        public async Task<IActionResult> Search(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar un nombre de autor.");
                return View("Index", new BookSearchViewModel());
            }

            var results = await _bookService.SearchByAuthorAsync(author);

            var model = new BookSearchViewModel
            {
                SearchResults = results
            };

            return View("Index", model);
        }

        // Página del historial
        [HttpGet]
        public async Task<IActionResult> Historial()
        {
            var historialResults = await _historialRepository.ObtenerHistorialAsync();
            return View(historialResults);
        }
    }
}