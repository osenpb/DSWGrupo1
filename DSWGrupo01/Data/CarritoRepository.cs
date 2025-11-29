using DSWGrupo01.Models;
using Microsoft.Data.SqlClient;

public class CarritoRepository
{
    private readonly string _cn;

    public CarritoRepository(IConfiguration config)
    {
        _cn = config.GetConnectionString("DSWGrupo01");
    }

    public int ObtenerCarritoUsuario(int Id_Usuario)
    {
        using (var con = new SqlConnection(_cn))
        {
            con.Open();
            var cmd = new SqlCommand(
                "SELECT Id FROM Carrito WHERE Id_Usuario=@u",
                con
            );
            cmd.Parameters.AddWithValue("@u", Id_Usuario);

            var result = cmd.ExecuteScalar();
            if (result != null)
                return Convert.ToInt32(result);

            cmd = new SqlCommand(
                "INSERT INTO Carrito (Id_Usuario) VALUES (@u); SELECT SCOPE_IDENTITY();",
                con
            );
            cmd.Parameters.AddWithValue("@u", Id_Usuario);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    // AGREGA ITEM
    public void AgregarProducto(int carritoId, int viniloId, decimal precio)
    {
        using (var con = new SqlConnection(_cn))
        {
            con.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO CarritoProducto(CarritoId,ViniloId,Cantidad,Precio)
                VALUES(@c,@v,1,@p)
            ", con);

            cmd.Parameters.AddWithValue("@c", carritoId);
            cmd.Parameters.AddWithValue("@v", viniloId);
            cmd.Parameters.AddWithValue("@p", precio);

            cmd.ExecuteNonQuery();
        }
    }

    public void CambiarCantidad(int itemId, int delta)
    {
        using (var con = new SqlConnection(_cn))
        {
            con.Open();
            var cmd = new SqlCommand(@"
                UPDATE CarritoProducto
                SET Cantidad = Cantidad + @d
                WHERE Id=@i
            ", con);

            cmd.Parameters.AddWithValue("@d", delta);
            cmd.Parameters.AddWithValue("@i", itemId);

            cmd.ExecuteNonQuery();
        }
    }

    public void EliminarItem(int id)
    {
        using (var con = new SqlConnection(_cn))
        {
            con.Open();
            new SqlCommand("DELETE FROM CarritoProducto WHERE Id=@id", con)
            {
                Parameters = { new SqlParameter("@id", id) }
            }.ExecuteNonQuery();
        }
    }

    public List<CarritoProductoViewModel> ObtenerCarrito(int carritoId)
    {
        var lista = new List<CarritoProductoViewModel>();

        using (var con = new SqlConnection(_cn))
        {
            con.Open();
            var cmd = new SqlCommand(@"
                SELECT CP.Id, CP.ViniloId, V.titulo, V.imagen_url, CP.Precio, CP.Cantidad
                FROM CarritoProducto CP
                INNER JOIN Vinilo V ON V.Id = CP.ViniloId
                WHERE CP.CarritoId = @c
            ", con);

            cmd.Parameters.AddWithValue("@c", carritoId);

            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                lista.Add(new CarritoProductoViewModel
                {
                    Id = rd.GetInt32(0),
                    ViniloId = rd.GetInt32(1),
                    Titulo = rd.GetString(2),
                    Imagen = rd.GetString(3),
                    Precio = rd.GetDecimal(4),
                    Cantidad = rd.GetInt32(5)
                });
            }
        }

        return lista;
    }
}

