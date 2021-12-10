using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;
using Packing.Shared.Pedidos;

namespace Packing.Negocio.Pedidos
{
    public class ObtenerInfoPedido : IRequestHandler<ObtenerInfoPedidoDto,Pedido>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerInfoPedido(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> Handle(ObtenerInfoPedidoDto request, CancellationToken cancellationToken)
        {
            return await _context.Pedidos.Where(x => x.GuidPedido.Equals(request.GuidPedido))
                .Include(x => x.EmpresaMandante)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
