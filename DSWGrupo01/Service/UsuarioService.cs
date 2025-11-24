using DSWGrupo01.Data;
using DSWGrupo01.Models;

namespace DSWGrupo01.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;

        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> RegistrarAsync(UsuarioModel u)
        {
            return await _repo.RegistrarAsync(u);
        }

        public Task<UsuarioModel?> ValidarLoginAsync(string email, string pass)
        {
            return _repo.ValidarLoginAsync(email, pass);
        }

        public async Task<UsuarioModel?> ObtenerPorIdAsync(int idUsuario)
        {
            return await _repo.ObtenerPorIdAsync(idUsuario);
        }
    }
}
