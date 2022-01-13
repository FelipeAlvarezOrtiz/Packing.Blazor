using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Persistencia;
using Packing.Shared.EmpresaDto;

namespace Packing.Negocio.Empresas
{
    public class DeshabilitarEmpresa : IRequestHandler<DeshabilitarEmpresaCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeshabilitarEmpresa(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeshabilitarEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = await _context.Empresas.Where(x => x.IdEmpresa == request.IdEmpresa).Include(x => x.ProductosVisibles)
                .ThenInclude(p => p.Producto).FirstOrDefaultAsync(cancellationToken);
            if (empresa is null)
                throw new Exception("La empresa no existe");
            empresa.EmpresaActiva = !empresa.EmpresaActiva;

            _context.Empresas.Update(empresa);

            return await _context.SaveChangesAsync(cancellationToken) > 0 
                ? Unit.Value 
                : throw new Exception("No se ha podido guardar los datos.");
        }
    }
}
