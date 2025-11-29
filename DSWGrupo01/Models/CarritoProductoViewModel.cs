namespace DSWGrupo01.Models
{
    public class CarritoProductoViewModel
    {
        public int Id { get; set; }
        public int ViniloId { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal => Precio * Cantidad;
    }
}
