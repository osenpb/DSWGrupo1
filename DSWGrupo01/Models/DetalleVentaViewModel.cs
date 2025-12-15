namespace DSWGrupo01.Models
{
    public class DetalleVentaViewModel
    {
        public int Id_Vinilo { get; set; }
        public string Titulo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal => Cantidad * Precio_Unitario;
    }
}
