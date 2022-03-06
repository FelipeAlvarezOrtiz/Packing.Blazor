using MediatR;
using Packing.Core.Empresas;

namespace Packing.Shared.EmpresaDto
{
    public class ObtenerEmpresaQuery : IRequest<Empresa>
    {
        public int IdEmpresa { get; set; }
    }

    public class ObtenerEmpresaQueryPorRut : IRequest<Empresa>
    {
        public string RutEmpresa { get; set; }
    }

}
