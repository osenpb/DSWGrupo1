using DSWGrupo01.Exceptions;
using DSWGrupo01.Models;
using DSWGrupo01.Repositories;

namespace DSWGrupo01.Service
{
    public class ViniloService
    {
        const string EntityName = "Vinilo";

        private readonly ViniloRepository _repository;
        

        public ViniloService(ViniloRepository repository)
        {
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

        public async Task AgregarViniloAsync(ViniloModel vinilo)
        {
            if (vinilo.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            await _repository.AgregarViniloAsync(vinilo);
        }

        public async Task ActualizarViniloAsync(ViniloModel vinilo)
        {
            var existente = await _repository.ObtenerViniloPorIdAsync(vinilo.Id);
            if (existente == null)
                throw new EntityNotFoundException(EntityName, vinilo.Id);

            await _repository.ActualizarViniloAsync(vinilo);
        }

        public async Task EliminarViniloAsync(int id)
        {
            var existente = await _repository.ObtenerViniloPorIdAsync(id);
            if (existente == null)
                throw new EntityNotFoundException(EntityName, id);

            await _repository.EliminarViniloAsync(id);
        }
    }

}
