namespace DSWGrupo01.Models
{
    public class CarritoProducto{
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int ViniloId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
