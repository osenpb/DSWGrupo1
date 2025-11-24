using DSWGrupo01.Models;
using DSWGrupo01.Service;
using Microsoft.AspNetCore.Mvc;

namespace DSWGrupo01.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        // GET: /Usuario/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Usuario/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string contrasenia)
        {
            var user = await _service.ValidarLoginAsync(email, contrasenia);

            if (user != null)
            {
                HttpContext.Session.SetString("usuario", user.Nombre);
                HttpContext.Session.SetInt32("idUsuario", user.Id_Usuario);
                HttpContext.Session.SetInt32("rol", user.Id_Rol);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        // GET: /Usuario/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Usuario/Register
        [HttpPost]
        public async Task<IActionResult> Register(UsuarioModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Id_Rol = 2;

            await _service.RegistrarAsync(model);

            TempData["msg"] = "Usuario registrado correctamente";
            return RedirectToAction("Login");
        }
    }
}
