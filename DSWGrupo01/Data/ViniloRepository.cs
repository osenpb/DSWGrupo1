using DSWGrupo01.Models;
using Microsoft.Data.SqlClient;

namespace DSWGrupo01.Repositories
{
    public class ViniloRepository
    {
        private readonly string _connectionString;
        const string TablaVinilo = "vinilo";

        public ViniloRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DSWGrupo01");
        }

        public async Task AgregarViniloAsync(ViniloModel vinilo)
        {
            // ✅ Nombres de columnas en minúsculas
            var sql = $"INSERT INTO {TablaVinilo} (titulo, artista, anio, precio, stock, preview, imagen_url, descripcion, fecha_ingreso) " +
                      "VALUES (@Titulo, @Artista, @Anio, @Precio, @Stock, @Preview, @ImagenUrl,@Descripcion, @FechaIngreso)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Titulo", vinilo.Titulo);
                    cmd.Parameters.AddWithValue("@Artista", vinilo.Artista);
                    cmd.Parameters.AddWithValue("@Anio", vinilo.Anio);
                    cmd.Parameters.AddWithValue("@Precio", vinilo.Precio);
                    cmd.Parameters.AddWithValue("@Stock", vinilo.Stock);
                    cmd.Parameters.AddWithValue("@Preview", vinilo.Preview);
                    cmd.Parameters.AddWithValue("@ImagenUrl", vinilo.ImagenUrl);
                    cmd.Parameters.AddWithValue("@Descripcion", vinilo.Descripcion);
                    cmd.Parameters.AddWithValue("@FechaIngreso", DateTime.Now);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el vinilo a la base de datos.", ex);
            }
        }

        public async Task<List<ViniloModel>> ObtenerVinilosAsync()
        {
            var vinilos = new List<ViniloModel>();
            // ✅ Nombres de columnas en minúsculas + TRIM para NCHAR
            var sql = $"SELECT Id, TRIM(titulo) as titulo, TRIM(artista) as artista, anio, precio, stock, " +
                      $"TRIM(preview) as preview, imagen_url,TRIM(descripcion) as descripcion, fecha_ingreso FROM {TablaVinilo}";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var vinilo = new ViniloModel
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Artista = reader.GetString(2),
                                Anio = reader.GetDateTime(3), 
                                Precio = reader.GetDecimal(4),
                                Stock = reader.GetInt32(5),
                                Preview = reader.GetString(6),
                                ImagenUrl = reader.GetString(7),
                                Descripcion = reader.GetString(8),
                                FechaIngreso = reader.GetDateTime(9).ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            vinilos.Add(vinilo);
                        }
                    }
                }
                return vinilos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los vinilos desde la base de datos.", ex);
            }
        }

        public async Task<ViniloModel> ObtenerViniloPorIdAsync(int id)
        {
            var sql = $"SELECT Id, TRIM(titulo) as titulo, TRIM(artista) as artista, anio, precio, stock, " +
                      $"TRIM(preview) as preview, imagen_url,TRIM(descripcion) as descripcion, fecha_ingreso " +
                      $"FROM {TablaVinilo} WHERE Id = @Id";

            try
            {
                ViniloModel vinilo = null;

                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await connection.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            vinilo = new ViniloModel
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Artista = reader.GetString(2),
                                Anio = reader.GetDateTime(3), 
                                Precio = reader.GetDecimal(4),
                                Stock = reader.GetInt32(5),
                                Preview = reader.GetString(6),
                                ImagenUrl = reader.GetString(7),
                                Descripcion = reader.GetString(8),
                                FechaIngreso = reader.GetDateTime(9).ToString("yyyy-MM-dd HH:mm:ss")
                            };
                        }
                    }
                }
                return vinilo;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el vinilo desde la base de datos.", ex);
            }
        }

        public async Task EliminarViniloAsync(int id)
        {
            var sql = $"DELETE FROM {TablaVinilo} WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el vinilo de la base de datos.", ex);
            }
        }

        public async Task<bool> ActualizarViniloAsync(ViniloModel viniloModel)
        {

            var sql = $"UPDATE {TablaVinilo} SET titulo = @Titulo, artista = @Artista, anio = @Anio, " +
                      $"precio = @Precio, stock = @Stock, preview = @Preview, imagen_url= @ImagenUrl , descripcion = @Descripcion " +
                      $"WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", viniloModel.Id);
                    cmd.Parameters.AddWithValue("@Titulo", viniloModel.Titulo);
                    cmd.Parameters.AddWithValue("@Artista", viniloModel.Artista);
                    cmd.Parameters.AddWithValue("@Anio", viniloModel.Anio);
                    cmd.Parameters.AddWithValue("@Precio", viniloModel.Precio);
                    cmd.Parameters.AddWithValue("@Stock", viniloModel.Stock);
                    cmd.Parameters.AddWithValue("@Preview", viniloModel.Preview);
                    cmd.Parameters.AddWithValue("@ImagenUrl", viniloModel.ImagenUrl);
                    cmd.Parameters.AddWithValue("@Descripcion", viniloModel.Descripcion);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el vinilo en la base de datos.", ex);
            }
        }
    }
}