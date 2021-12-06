using System.Collections.Generic;
using MediatR;
using Packing.Core.Productos;

namespace Packing.Shared.Presentaciones
{
    public record BuscarPresentacionesDelFormatoGrupo : IRequest<List<Presentacion>>
    {
        public int IdFormato { get; set; }
        public int IdGrupo { get; set; }
    }
}
