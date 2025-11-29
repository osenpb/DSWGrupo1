using DSWGrupo01.Service;
using Microsoft.AspNetCore.Mvc;

namespace DSWGrupo01.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoService _cart;
        private readonly ViniloService _vinilos;

        public CarritoController(CarritoService cart, ViniloService vinilos)
        {
            _cart = cart;
            _vinilos = vinilos;
        }

        private int? IdUsuario =>
            HttpContext.Session.GetInt32("idUsuario");

        public IActionResult Index()
        {
            if (IdUsuario == null)
                return RedirectToAction("Login", "Usuario");

            int carritoId = _cart.ObtenerCarritoUsuario(IdUsuario.Value);
            var items = _cart.ObtenerItems(carritoId);
            return View(items);
        }

        public async Task<IActionResult> Agregar(int viniloId)
        {
            if (IdUsuario == null)
                return RedirectToAction("Login", "Usuario");

            int carritoId = _cart.ObtenerCarritoUsuario(IdUsuario.Value);

            var v = await _vinilos.ObtenerViniloPorIdAsync(viniloId);

            if (v == null)
                return NotFound();

            _cart.AgregarProducto(carritoId, viniloId, v.Precio);

            return RedirectToAction("Index");
        }

        public IActionResult CambiarCantidad(int id, int delta)
        {
            if (IdUsuario == null)
                return RedirectToAction("Login", "Usuario");

            _cart.CambiarCantidad(id, delta);
            return Ok();
        }

        public IActionResult Eliminar(int id)
        {
            if (IdUsuario == null)
                return RedirectToAction("Login", "Usuario");

            _cart.EliminarItem(id);
            return RedirectToAction("Index");
        }
    }
}