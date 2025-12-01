using DSWGrupo01.Data;
using DSWGrupo01.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DSWGrupo01.Service
{
    public class VentaService
    {
        private readonly VentaRepository _repo;

        public VentaService(VentaRepository repo)
        {
            _repo = repo;
        }

        public async Task CargarCarrito(PagoModel model, int idUsuario)
        {
            var carrito = await _repo.ObtenerCarritoPorUsuario(idUsuario);

            if (carrito == null)
            {
                model.Id_Carrito = null;
                model.Items = new List<CarritoProductoViewModel>();
                model.Total = 0;
                return;
            }

            model.Id_Carrito = carrito.Id;

            var productos = await _repo.ObtenerProductosDelCarrito(carrito.Id);

            model.Items = productos;
            model.Total = productos.Sum(p => p.Subtotal);
        }


        // Procesar pago: crea usuario si se necesita
        public async Task ProcesarPagoAsync(PagoModel model, UsuarioService usuarioService, HttpContext httpContext)
        {
            int? idUsuario = model.Id_Usuario;

            var idSesion = httpContext.Session.GetInt32("idUsuario");
            if (idSesion != null)
            {
                idUsuario = idSesion.Value;
            }

            if (model.CrearCuenta && idUsuario == null)
            {
                var nuevoUsuario = new UsuarioModel
                {
                    Nombre = model.Nombre,
                    Email = model.Email,
                    Contrasenia = model.Contrasenia,
                    Dni = model.Dni,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Id_Rol = 2 // Cliente
                };

                // Registrar y obtener Id generado
                idUsuario = await usuarioService.RegistrarAsync(nuevoUsuario);

                httpContext.Session.SetString("usuario", nuevoUsuario.Nombre);
                httpContext.Session.SetInt32("idUsuario", idUsuario.Value);
                httpContext.Session.SetInt32("rol", nuevoUsuario.Id_Rol);
            }

            await _repo.CrearVentaAsync(model, idUsuario);

            if (model.Id_Carrito.HasValue)
                await _repo.EliminarCarritoAsync(model.Id_Carrito.Value);
        }
    }
}
