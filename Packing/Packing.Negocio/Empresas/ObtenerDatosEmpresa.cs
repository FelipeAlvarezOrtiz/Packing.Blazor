using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Empresas;
using Packing.Persistencia;
using Packing.Shared.EmpresaDto;

namespace Packing.Negocio.Empresas
{
    public class ObtenerDatosEmpresa : IRequestHandler<ObtenerEmpresaQuery,Empresa>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerDatosEmpresa(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Empresa> Handle(ObtenerEmpresaQuery request, CancellationToken cancellationToken)
        {
            return await _context.Empresas.Where(x => x.IdEmpresa == request.IdEmpresa)
                .Include(x => x.ProductosVisibles).ThenInclude(x => x.Producto)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
