using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Usuarios;
using Packing.Persistencia;

namespace Packing.Negocio.Cargos
{
    public class ObtenerCargoPorNombre
    {
        public record Command : IRequest<CargoInterno>
        {
            public string NombreCargo { get; set; }
        }

        public class Handler : IRequestHandler<Command, CargoInterno>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CargoInterno> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _context.CargosInternos.Where(cargo => cargo.NombreCargo.Equals(request.NombreCargo))
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
