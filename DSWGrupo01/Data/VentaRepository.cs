using DSWGrupo01.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DSWGrupo01.Data
{
    public class VentaRepository
    {
        private readonly string _connectionString;

        public VentaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DSWGrupo01");
        }

        // Obtener carrito por sessionId (invitados)
        //public async Task<Carrito?> ObtenerCarritoPorSession(string sessionId)
        //{
        //    Carrito? carrito = null;

        //    var sql = @"SELECT Id, sessionId, fecha_ingreso 
        //                FROM Carrito 
        //                WHERE sessionId = @sessionId";

        //    using (var conn = new SqlConnection(_connectionString))
        //    using (var cmd = new SqlCommand(sql, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@sessionId", sessionId);

        //        await conn.OpenAsync();
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            if (await reader.ReadAsync())
        //            {
        //                carrito = new Carrito
        //                {
        //                    Id = reader.GetInt32(0),
        //                    Id_Usuario = reader.GetInt32(1),
        //                    Fecha_Ingreso = reader.GetDateTime(2)
        //                };
        //            }
        //        }
        //    }

        //    return carrito;
        //}

        public async Task<Carrito?> ObtenerCarritoPorUsuario(int id)
        {
            Carrito? carrito = null;

            var sql = @"SELECT * FROM Carrito WHERE Id_usuario = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        carrito = new Carrito
                        {
                            Id = reader.GetInt32(0),
                            Id_Usuario = reader.GetInt32(1),
                            Fecha_Ingreso = reader.GetDateTime(2)
                        };
                    }
                }
            }

            return carrito;
        }

        public async Task<List<CarritoProductoViewModel>> ObtenerProductosDelCarrito(int id)
        {
            var items = new List<CarritoProductoViewModel>();

            var sql = @"SELECT CP.Id, CP.ViniloId, V.titulo, V.imagen_url, CP.Precio, CP.Cantidad
                        FROM CarritoProducto CP
                        INNER JOIN Vinilo V ON V.Id = CP.ViniloId
                        WHERE CP.CarritoId = @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new CarritoProductoViewModel
                        {
                            Id = reader.GetInt32(0),
                            ViniloId = reader.GetInt32(1),
                            Titulo = reader.GetString(2),
                            Imagen = reader.GetString(3),
                            Precio = reader.GetDecimal(4),
                            Cantidad = reader.GetInt32(5)
                        });
                    }
                }
            }

            return items;
        }

        public async Task<int> CrearVentaAsync(PagoModel model, int? idUsuario)
        {
            int idVenta;

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                var sqlVenta = @"
                    INSERT INTO Venta
                    (Id_Usuario, Nombre_Destinatario, Email_Destinatario, Telefono_Destinatario, Direccion_Envio, Total, Metodo_Pago)
                    OUTPUT INSERTED.Id_Venta
                    VALUES (@IdUsuario, @Nombre, @Email, @Telefono, @Direccion, @Total, @MetodoPago)";

                using (var cmd = new SqlCommand(sqlVenta, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", (object)idUsuario ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion",
                        model.UsarDireccionDiferente && !string.IsNullOrEmpty(model.Direccion_Envio)
                            ? model.Direccion_Envio
                            : model.Direccion
                    );
                    cmd.Parameters.AddWithValue("@Total", model.Total);
                    cmd.Parameters.AddWithValue("@MetodoPago", model.Metodo_Pago);

                    idVenta = (int)await cmd.ExecuteScalarAsync();
                }

                foreach (var item in model.Items)
                {
                    var sqlDetalle = @"
                        INSERT INTO DetalleVenta
                        (Id_Venta, Id_Vinilo, Cantidad, Precio_Unitario)
                        VALUES (@IdVenta, @IdVinilo, @Cantidad, @PrecioUnitario)";

                    using (var cmdDet = new SqlCommand(sqlDetalle, conn))
                    {
                        cmdDet.Parameters.AddWithValue("@IdVenta", idVenta);
                        cmdDet.Parameters.AddWithValue("@IdVinilo", item.ViniloId);
                        cmdDet.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdDet.Parameters.AddWithValue("@PrecioUnitario", item.Precio);

                        await cmdDet.ExecuteNonQueryAsync();
                    }
                }
            }

            return idVenta;
        }

        public async Task EliminarCarritoAsync(int id)
        {
            string sqlDetalles = "DELETE FROM CarritoProducto WHERE CarritoId = @IdCarrito";
            string sqlCarrito = "DELETE FROM Carrito WHERE Id = @IdCarrito";

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (var cmd1 = new SqlCommand(sqlDetalles, conn))
                using (var cmd2 = new SqlCommand(sqlCarrito, conn))
                {
                    cmd1.Parameters.AddWithValue("@IdCarrito", id);
                    cmd2.Parameters.AddWithValue("@IdCarrito", id);

                    await cmd1.ExecuteNonQueryAsync();
                    await cmd2.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
