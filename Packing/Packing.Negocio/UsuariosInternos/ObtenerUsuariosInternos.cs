using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Usuarios;
using Packing.Persistencia;
using Packing.Shared.Usuarios;

namespace Packing.Negocio.UsuariosInternos
{
	public class ObtenerUsuariosInternos : IRequestHandler<ObtenerUsuariosInternosQuery,List<UsuarioInterno>>
    {
        private readonly ApplicationDbContext _context;
        public ObtenerUsuariosInternos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioInterno>> Handle(ObtenerUsuariosInternosQuery request, CancellationToken cancellationToken)
        {
            return await _context.UsuariosInternos.Include(x => x.Cargo).ToListAsync(cancellationToken);
        }
    }
}
