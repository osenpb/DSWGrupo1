using DSWGrupo01.Models;
using Microsoft.Data.SqlClient;

namespace DSWGrupo01.Data
{
    public class CarritoRepository
    {
        private readonly string _connectionString;

        public CarritoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DSWGrupo01");
        }

        public async Task<int?> ObtenerCarritoPorSession(string sessionId)
        {
            var query = "SELECT Id FROM Carrito WHERE sessionId = @sessionId";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@sessionId", sessionId);
                await con.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();
                return result == null ? null : (int?)result;
            }
        }

        public async Task<int> CrearCarrito(string sessionId)
        {
            var query = @"INSERT INTO Carrito (sessionId) 
                          OUTPUT INSERTED.Id 
                          VALUES (@sessionId)";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@sessionId", sessionId);
                await con.OpenAsync();

                return (int)await cmd.ExecuteScalarAsync();
            }
        }

        public async Task AgregarProducto(int carritoId, int viniloId, int cantidad, decimal precio)
        {
            var query = @"INSERT INTO CarritoProducto 
                        (carritoId, viniloId, cantidad, precio)
                        VALUES (@carritoId, @viniloId, @cantidad, @precio)";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@carritoId", carritoId);
                cmd.Parameters.AddWithValue("@viniloId", viniloId);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@precio", precio);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<CarritoProducto>> ObtenerProductos(int carritoId)
        {
            var productos = new List<CarritoProducto>();

            var query = @"SELECT * FROM CarritoProducto WHERE carritoId = @carritoId";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@carritoId", carritoId);

                await con.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    productos.Add(new CarritoProducto
                    {
                        Id = (int)reader["Id"],
                        carritoId = (int)reader["carritoId"],
                        viniloId = (int)reader["viniloId"],
                        cantidad = (int)reader["cantidad"],
                        precio = (decimal)reader["precio"]
                    });
                }
            }
            return productos;
        }

        public async Task UpdateCantidad(int id, int nuevaCantidad)
        {
            var query = @"UPDATE CarritoProducto SET cantidad=@cantidad WHERE Id=@id";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@cantidad", nuevaCantidad);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarItem(int id)
        {
            var query = "DELETE FROM CarritoProducto WHERE Id=@id";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Vaciar(int carritoId)
        {
            var query = "DELETE FROM CarritoProducto WHERE carritoId=@carritoId";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@carritoId", carritoId);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
