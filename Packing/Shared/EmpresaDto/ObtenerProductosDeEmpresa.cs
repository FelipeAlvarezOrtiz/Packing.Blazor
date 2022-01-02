using System.Collections.Generic;
using MediatR;
using Packing.Core.Empresas;

namespace Packing.Shared.EmpresaDto
{
    public class ObtenerProductosDeEmpresa : IRequest<List<DisponibilidadProducto>>
    {
        public int IdEmpresa { get; set; }
    }
}
