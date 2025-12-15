using DSWGrupo01.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DSWGrupo01.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DSWGrupo01");
        }

        // Registrar usuario y devolver Id generado
        public async Task<int> RegistrarAsync(UsuarioModel u)
        {
            string sql = @"
                INSERT INTO Usuario 
                (Id_Rol, Nombre, Email, Contrasenia, Dni, Direccion, Telefono)
                OUTPUT INSERTED.Id_Usuario
                VALUES 
                (@Id_Rol, @Nombre, @Email, @Contrasenia, @Dni, @Direccion, @Telefono)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id_Rol", u.Id_Rol);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Contrasenia", u.Contrasenia);
                cmd.Parameters.AddWithValue("@Dni", u.Dni);
                cmd.Parameters.AddWithValue("@Direccion", u.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", u.Telefono);

                await conn.OpenAsync();
                u.Id_Usuario = (int)await cmd.ExecuteScalarAsync();
            }

            return u.Id_Usuario;
        }

        public async Task<UsuarioModel?> ValidarLoginAsync(string email, string pass)
        {
            UsuarioModel? user = null;
            string sql = @"
                SELECT Id_Usuario, Id_Rol, Nombre, Email, Dni, Direccion, Telefono, Fecha_Registro
                FROM Usuario
                WHERE Email = @Email AND Contrasenia = @Pass";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Pass", pass);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new UsuarioModel
                        {
                            Id_Usuario = reader.GetInt32(0),
                            Id_Rol = reader.GetInt32(1),
                            Nombre = reader.GetString(2),
                            Email = reader.GetString(3),
                            Dni = reader.GetString(4),
                            Direccion = reader.GetString(5),
                            Telefono = reader.GetString(6),
                            Fecha_Registro = reader.GetDateTime(7)
                        };
                    }
                }
            }
            return user;
        }

        public async Task<UsuarioModel?> ObtenerPorIdAsync(int idUsuario)
        {
            UsuarioModel? user = null;
            string sql = @"
                SELECT Id_Usuario, Id_Rol, Nombre, Email, Contrasenia, Dni, Direccion, Telefono, Fecha_Registro
                FROM Usuario
                WHERE Id_Usuario = @IdUsuario";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new UsuarioModel
                        {
                            Id_Usuario = reader.GetInt32(0),
                            Id_Rol = reader.GetInt32(1),
                            Nombre = reader.GetString(2),
                            Email = reader.GetString(3),
                            Contrasenia = reader.GetString(4),
                            Dni = reader.GetString(5),
                            Direccion = reader.GetString(6),
                            Telefono = reader.GetString(7),
                            Fecha_Registro = reader.GetDateTime(8)
                        };
                    }
                }
            }
            return user;
        }

        public async Task ActualizarPerfilAsync(UsuarioPerfilViewModel model)
        {
            string sql = @"UPDATE Usuario SET
                            Nombre = @Nombre,
                            Direccion = @Direccion,
                            Telefono = @Telefono{0}
                        WHERE Id_Usuario = @IdUsuario";

            string passSql = string.IsNullOrEmpty(model.NuevaContrasenia) ? "" : ", Contrasenia = @Contrasenia";

            sql = string.Format(sql, passSql);

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", model.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", model.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", model.Telefono);
                cmd.Parameters.AddWithValue("@IdUsuario", model.Id_Usuario);

                if (!string.IsNullOrEmpty(model.NuevaContrasenia))
                    cmd.Parameters.AddWithValue("@Contrasenia", model.NuevaContrasenia);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
