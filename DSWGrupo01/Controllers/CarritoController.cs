using DSWGrupo01.Service;
using Microsoft.AspNetCore.Mvc;

namespace DSWGrupo01.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _service;

        public CarritoController(CarritoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            string sessionId = HttpContext.Session.Id;
            int carritoId = await _service.ObtenerOCrear(sessionId);

            var items = await _service.ObtenerItems(carritoId);
            return View(items);
        }

        public async Task<IActionResult> Agregar(int viniloId, decimal precio)
        {
            string sessionId = HttpContext.Session.Id;
            int carritoId = await _service.ObtenerOCrear(sessionId);

            await _service.Agregar(carritoId, viniloId, precio);

            return RedirectToAction("Index", "Carrito");
        }

        public async Task<IActionResult> Mas(int id)
        {
            // ID del CarritoProducto
            string sessionId = HttpContext.Session.Id;
            int carritoId = await _service.ObtenerOCrear(sessionId);

            var items = await _service.ObtenerItems(carritoId);
            var item = items.First(x => x.Id == id);

            await _service.CambiarCantidad(id, item.cantidad + 1);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Menos(int id)
        {
            string sessionId = HttpContext.Session.Id;
            int carritoId = await _service.ObtenerOCrear(sessionId);

            var items = await _service.ObtenerItems(carritoId);
            var item = items.First(x => x.Id == id);

            if (item.cantidad > 1)
                await _service.CambiarCantidad(id, item.cantidad - 1);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Vaciar()
        {
            string sessionId = HttpContext.Session.Id;
            int carritoId = await _service.ObtenerOCrear(sessionId);

            await _service.Vaciar(carritoId);
            return RedirectToAction("Index");
        }
    }
}
