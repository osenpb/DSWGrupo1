using DSWGrupo01.Models;
using DSWGrupo01.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DSWGrupo01.Controllers
{
    public class ViniloController : Controller
    {
        private readonly ViniloService _viniloService;

        public ViniloController(ViniloService viniloService)
        {
            _viniloService = viniloService;

        }

        // GET: ViniloController
        public async Task<IActionResult> Index()
        {
            List<ViniloModel> vinilos = await _viniloService.ObtenerVinilosAsync();
            return View(vinilos);
        }

        // GET: ViniloController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: ViniloController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViniloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViniloModel viniloModel)
        {
            Console.WriteLine("=== LLEGÓ AL CONTROLLER ===");
            Console.WriteLine($"Titulo recibido: {viniloModel?.Titulo}");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            // Mostrar todos los errores de validación
            if (!ModelState.IsValid)
            {
                Console.WriteLine("=== ERRORES DE VALIDACIÓN ===");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}");
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"  Error: {err.ErrorMessage}");
                    }
                }
                return View(viniloModel);
            }

            try
            {
           
                // Guardar vinilo en la base de datos
                await _viniloService.AgregarViniloAsync(viniloModel);
                Console.WriteLine("=== VINILO GUARDADO ===");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Luego veo un Logcat
                Console.WriteLine("=== ERROR AL GUARDAR ===");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");

                ModelState.AddModelError("", $"Error: {ex.InnerException?.Message ?? ex.Message}");
                return View(viniloModel);
            }
        }

        // GET: ViniloController/Edit/5
        // GET: ViniloController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vinilo = await _viniloService.ObtenerViniloPorIdAsync(id);
            if (vinilo == null) return NotFound();

            return View(vinilo);
        }

        // POST: ViniloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ViniloModel viniloModel)
        {
            if (id != viniloModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _viniloService.ActualizarViniloAsync(viniloModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("=== ERROR AL ACTUALIZAR ===");
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", $"Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            return View(viniloModel);
        }


        // GET: ViniloController/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View();
        //}

        // POST: ViniloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            
            try
            {
                await _viniloService.EliminarViniloAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
