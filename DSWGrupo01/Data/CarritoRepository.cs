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
    public void AgregarProducto(int carritoId, int viniloId, int cantidad, decimal precio)
    {
        using (var con = new SqlConnection(_cn))
        {
            con.Open();

            var checkCmd = new SqlCommand(@"
            SELECT Id, Cantidad
            FROM CarritoProducto
            WHERE CarritoId = @c AND ViniloId = @v
        ", con);

            checkCmd.Parameters.AddWithValue("@c", carritoId);
            checkCmd.Parameters.AddWithValue("@v", viniloId);

            var rd = checkCmd.ExecuteReader();

            if (rd.Read())
            {
                int itemId = rd.GetInt32(0);
                rd.Close();

                var updateCmd = new SqlCommand(@"
                UPDATE CarritoProducto
                SET Cantidad = Cantidad + @k
                WHERE Id = @id
            ", con);

                updateCmd.Parameters.AddWithValue("@k", cantidad);
                updateCmd.Parameters.AddWithValue("@id", itemId);
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                rd.Close();

                var insertCmd = new SqlCommand(@"
                INSERT INTO CarritoProducto(CarritoId,ViniloId,Cantidad,Precio)
                VALUES(@c,@v,@k,@p)
            ", con);

                insertCmd.Parameters.AddWithValue("@c", carritoId);
                insertCmd.Parameters.AddWithValue("@v", viniloId);
                insertCmd.Parameters.AddWithValue("@k", cantidad);
                insertCmd.Parameters.AddWithValue("@p", precio);

                insertCmd.ExecuteNonQuery();
            }
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

