namespace DSWGrupo01.Models
{
    public class DetalleVentaModel
    {
        public int Id_Detalle_Venta { get; set; }
        public int Id_Venta { get; set; }
        public int Id_Vinilo{ get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario{ get; set; }
    }
}
