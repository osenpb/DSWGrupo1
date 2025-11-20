namespace DSWGrupo01.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string EntityName, int id)
            : base($"{EntityName} con {id} no fue encontrado.")
        {
        }

    }
}
