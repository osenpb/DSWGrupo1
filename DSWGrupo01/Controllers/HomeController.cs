using DSWGrupo01.Exceptions;
using DSWGrupo01.Models;
using DSWGrupo01.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DSWGrupo01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? q)
        {
            var vinilos = await _homeService.ObtenerVinilosAsync();

            if (!string.IsNullOrEmpty(q))
            {
                vinilos = vinilos
                    .Where(v => v.Titulo.Contains(q, StringComparison.OrdinalIgnoreCase)
                             || v.Artista.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Query = q;

            return View(vinilos);
        }

        // GET: /Home/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            try
            {
                var vinilo = await _homeService.ObtenerViniloPorIdAsync(id);
                return View(vinilo);
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"No se encontró el vinilo con ID {id}");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
