using Microsoft.AspNetCore.Mvc;
using DSWGrupo01.Models;
using DSWGrupo01.Service;

namespace DSWGrupo01.Controllers
{
    public class VentaController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly VentaService _pagoService;

        public VentaController(UsuarioService usuarioService, VentaService pagoService)
        {
            _usuarioService = usuarioService;
            _pagoService = pagoService;
        }

        // GET: /Venta/FinalizarCompra
        [HttpGet]
        public async Task<IActionResult> FinalizarCompra()
        {
            var model = new PagoModel();

            var idUsuario = HttpContext.Session.GetInt32("idUsuario");

            if (idUsuario != null)
            {
                var usuario = await _usuarioService.ObtenerPorIdAsync(idUsuario.Value);

                if (usuario != null)
                {
                    model.Id_Usuario = usuario.Id_Usuario;
                    model.Nombre = usuario.Nombre;
                    model.Dni = usuario.Dni;
                    model.Email = usuario.Email;
                    model.Telefono = usuario.Telefono;
                    model.Direccion = usuario.Direccion;
                }

                await _pagoService.CargarCarrito(model, idUsuario.Value);
            }
            else
            {
                // Usuario no logueado → carrito vacío, dejar hasta obtener carrito sin usuario logueado
                model.Items = new List<CarritoProducto>();
                model.Total = 0;
            }

            return View(model);
        }


        // POST: /Venta/FinalizarCompra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompra(PagoModel model)
        {
            var idUsuario = HttpContext.Session.GetInt32("idUsuario");

            if (idUsuario != null)
            {
                await _pagoService.CargarCarrito(model, idUsuario.Value);
            }
            else
            {
                model.Items = new List<CarritoProducto>();
                model.Total = 0;
            }

            // Validaciones para form de crear cuenta y cambiar direccion
            if (model.CrearCuenta && string.IsNullOrWhiteSpace(model.Contrasenia))
            {
                ModelState.AddModelError("Contrasenia", "Debe ingresar una contraseña si crea la cuenta.");
            }

            if (model.UsarDireccionDiferente && string.IsNullOrWhiteSpace(model.Direccion_Envio))
            {
                ModelState.AddModelError("Direccion_Envio", "Debe ingresar la dirección de envío si eligió diferente.");
            }

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

            await _pagoService.ProcesarPagoAsync(model, _usuarioService, HttpContext);

            TempData["Mensaje"] = "Compra realizada con éxito.";

            return RedirectToAction("Index", "Home");
        }
    }
}
