using DSWGrupo01.Exceptions;
using DSWGrupo01.Models;
using DSWGrupo01.Repositories;

namespace DSWGrupo01.Service
{
    public class HomeService
    {
        const string EntityName = "Vinilo";

        private readonly ViniloRepository _repository;

        public HomeService(ViniloRepository repository) {

            _repository = repository;
        }


        public async Task<List<ViniloModel>> ObtenerVinilosAsync()
        {
            return (await _repository.ObtenerVinilosAsync()).ToList();
        }


        public async Task<ViniloModel> ObtenerViniloPorIdAsync(int id)
        {
            var vinilo = await _repository.ObtenerViniloPorIdAsync(id);
            if (vinilo == null)
                throw new EntityNotFoundException(EntityName, id); // 
            return vinilo;
        }



    }
}
