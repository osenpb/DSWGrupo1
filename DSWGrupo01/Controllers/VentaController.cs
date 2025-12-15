using Microsoft.AspNetCore.Mvc;
using DSWGrupo01.Models;
using DSWGrupo01.Service;

namespace DSWGrupo01.Controllers
{
    public class VentaController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly VentaService _ventaService;

        private int? IdUsuario => HttpContext.Session.GetInt32("idUsuario");

        public VentaController(UsuarioService usuarioService, VentaService ventaService)
        {
            _usuarioService = usuarioService;
            _ventaService = ventaService;
        }

        // GET: /Venta/FinalizarCompra
        [HttpGet]
        public async Task<IActionResult> FinalizarCompra()
        {
            var model = new PagoModel();

            if (IdUsuario != null)
            {
                // Cargar datos del usuario
                var usuario = await _usuarioService.ObtenerPorIdAsync(IdUsuario.Value);

                if (usuario != null)
                {
                    model.Id_Usuario = usuario.Id_Usuario;
                    model.Nombre = usuario.Nombre;
                    model.Dni = usuario.Dni;
                    model.Email = usuario.Email;
                    model.Telefono = usuario.Telefono;
                    model.Direccion = usuario.Direccion;
                }

                // Cargar carrito del usuario
                await _ventaService.CargarCarrito(model, IdUsuario.Value);
            }
            else
            {
                // Carrito para un usuario no logueado (aún no implementado)
                model.Items = new List<CarritoProductoViewModel>();
                model.Total = 0;
            }

            return View(model);
        }


        // POST: /Venta/FinalizarCompra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompra(PagoModel model)
        {
            // Recargar carrito antes de validar o procesar
            if (IdUsuario != null)
            {
                await _ventaService.CargarCarrito(model, IdUsuario.Value);
            }
            else
            {
                model.Items = new List<CarritoProductoViewModel>();
                model.Total = 0;
            }

            // Validaciones adicionales
            if (model.CrearCuenta && string.IsNullOrWhiteSpace(model.Contrasenia))
            {
                ModelState.AddModelError("Contrasenia",
                    "Debe ingresar una contraseña si crea la cuenta.");
            }

            if (model.UsarDireccionDiferente && string.IsNullOrWhiteSpace(model.Direccion_Envio))
            {
                ModelState.AddModelError("Direccion_Envio",
                    "Debe ingresar la dirección de envío si eligió una diferente.");
            }

            // Si hay errores, volver a mostrar la vista con los datos
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["Errores"] = string.Join(" | ", errores);

                System.Diagnostics.Debug.WriteLine("Errores de validación: " + string.Join(", ", errores));

                return View(model);
            }

            // Procesar compra
            await _ventaService.ProcesarPagoAsync(model, _usuarioService, HttpContext);

            TempData["Mensaje"] = "Compra realizada con éxito.";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MisVentas()
        {
            var idUsuario = HttpContext.Session.GetInt32("idUsuario");
            var rol = HttpContext.Session.GetInt32("rol");

            if (idUsuario == null || rol == null)
                return RedirectToAction("Login", "Usuario");

            var ventas = await _ventaService.ListarVentasAsync(idUsuario, rol.Value);
            return View(ventas);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleVenta(int id)
        {
            var detalles = await _ventaService.ObtenerDetalleVentaAsync(id);
            ViewBag.IdVenta = id;
            return View(detalles);
        }
    }
}
