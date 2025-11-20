using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DSWGrupo01.Controllers
{

    // Test/TestConexion es la ruta para probar la conexion a la bd, lo dejo xseaca

    public class TestController : Controller
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DSWGrupo01;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

        public IActionResult TestConexion()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                return Content("¡Conexión exitosa!");
            }
            catch (Exception ex)
            {
                return Content($"Error al conectar: {ex.Message}");
            }
        }
    }
}
