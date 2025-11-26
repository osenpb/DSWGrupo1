namespace DSWGrupo01.Models
{
    public class VentaModel
    {
        public int Id_Venta { get; set; }
        public int? Id_Usuario { get; set; }
        public string Nombre_Destinatario { get; set; }
        public string Email_Destinatario { get; set; }
        public string Telefono_Destinatario { get; set; }
        public string Direccion_Envio { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Metodo_Pago { get; set; }
        public List<DetalleVentaModel> Detalles { get; set; } = new List<DetalleVentaModel>();
    }
}
