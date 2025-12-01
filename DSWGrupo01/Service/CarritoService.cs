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

        public int ObtenerCarritoUsuario(int Id_Usuario)
            => _repo.ObtenerCarritoUsuario(Id_Usuario);

        public void AgregarProducto(int carritoId, int viniloId, int cantidad, decimal precio)
            => _repo.AgregarProducto(carritoId, viniloId, cantidad, precio);

        public void CambiarCantidad(int itemId, int delta)
            => _repo.CambiarCantidad(itemId, delta);

        public void EliminarItem(int id)
            => _repo.EliminarItem(id);

        public List<CarritoProductoViewModel> ObtenerItems(int carritoId)
            => _repo.ObtenerCarrito(carritoId);
    }
}

