using System;
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
	public class ObtenerCargosInternos : IRequestHandler<ObtenerCargosInternosQuery, List<CargoInterno>>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerCargosInternos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CargoInterno>> Handle(ObtenerCargosInternosQuery request, CancellationToken cancellationToken)
        {
            return await _context.CargosInternos.ToListAsync(cancellationToken);
        }
	}
}
