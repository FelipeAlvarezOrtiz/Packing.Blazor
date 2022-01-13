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
    public class ActualizarEmpresa : IRequestHandler<ActualizarEmpresaCommand>
    {
        private readonly ApplicationDbContext _context;

        public ActualizarEmpresa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ActualizarEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = await _context.Empresas.Include(x => x.ProductosVisibles)
                .ThenInclude(x => x.Producto).Where(x => x.IdEmpresa == request.IdEmpresa)
                .FirstOrDefaultAsync(cancellationToken);
            if (empresa == null)
                throw new Exception("La empresa no existe");
            empresa.Direccion = request.Direccion;
            empresa.NombreEmpresa = request.NombreEmpresa;
            empresa.RazonSocial = request.RazonSocial;
            empresa.RutEmpresa = request.RutEmpresa;
            empresa.PersonaContacto = request.PersonaContacto;

            _context.Empresas.Update(empresa);

            return await _context.SaveChangesAsync(cancellationToken) > 0 ? Unit.Value : throw new Exception("Ha ocurrido un error al actualizar los datos.");
        }
    }
}
