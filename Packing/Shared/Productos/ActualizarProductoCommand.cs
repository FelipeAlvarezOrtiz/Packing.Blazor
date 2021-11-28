using MediatR;
using Packing.Shared.RespuestaEstandar;

namespace Packing.Shared.Productos
{
    public record ActualizarProductoCommand : IRequest<RespuestaContainer<int>>
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdFormato { get; set; }
        public int IdGrupo { get; set; }
        public int IdPresentacion { get; set; }
        public bool AfectoVencimiento { get; set; }
        public int DiaMinVencimiento { get; set; }
        public int DiaMaxVencimiento { get; set; }
    }
}
