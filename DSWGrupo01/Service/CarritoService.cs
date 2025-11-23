using DSWGrupo01.Data;
using DSWGrupo01.Models;

namespace DSWGrupo01.Service
{
    public class CarritoService
    {
        private readonly CarritoRepository _repo;

        public CarritoService(CarritoRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> ObtenerOCrear(string sessionId)
        {
            var carrito = await _repo.ObtenerCarritoPorSession(sessionId);

            if (carrito.HasValue)
                return carrito.Value;

            return await _repo.CrearCarrito(sessionId);
        }

        public async Task Agregar(int carritoId, int viniloId, decimal precio)
        {
            await _repo.AgregarProducto(carritoId, viniloId, 1, precio);
        }

        public async Task<List<CarritoProducto>> ObtenerItems(int carritoId)
        {
            return await _repo.ObtenerProductos(carritoId);
        }

        public async Task CambiarCantidad(int id, int cantidad)
        {
            await _repo.UpdateCantidad(id, cantidad);
        }

        public async Task Eliminar(int id)
        {
            await _repo.EliminarItem(id);
        }

        public async Task Vaciar(int carritoId)
        {
            await _repo.Vaciar(carritoId);
        }
    }
}
