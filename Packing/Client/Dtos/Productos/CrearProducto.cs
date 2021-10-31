namespace Packing.Client.Dtos.Productos
{
    public record CrearProducto
    {
        public string NombreProducto { get; set; }
        public string RutaImagen { get; set; }
        public int IdPresentacion { get; set; }
        public int IdFormato { get; set; }
        public int IdGrupo { get; set; }
        public bool AfectaVencimiento { get; set; }
        public int MinDiaVencimiento { get; set; }
        public int MaxDiaVencimiento { get; set; }
    };
}
