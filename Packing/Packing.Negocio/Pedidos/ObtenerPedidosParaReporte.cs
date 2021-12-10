using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Pedidos;
using Packing.Persistencia;
using Packing.Shared.ReportesDto;

namespace Packing.Negocio.Pedidos
{
    public class ObtenerPedidosParaReporte : IRequestHandler<BuscarPedidosReporteQuery,List<Pedido>>
    {
        private readonly ApplicationDbContext _context;

        public ObtenerPedidosParaReporte(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> Handle(BuscarPedidosReporteQuery request, CancellationToken cancellationToken)
        {
            var empresa = request.IdEmpresa;
            if (empresa == 0) return await ObtenerPedidosDeTodas(request.FechaDesde,request.FechaHasta,cancellationToken);
            return await ObtenerPedidosDeEmpresa(empresa, request.FechaDesde, request.FechaHasta, cancellationToken);
        }

        private async Task<List<Pedido>> ObtenerPedidosDeTodas(DateTime fechaDesde,DateTime fechaHasta,CancellationToken cancellationToken)
        {
            return await _context.Pedidos.Include(x => x.EmpresaMandante)
                .Where(x => (DateTime.Compare(x.FechaPedido, fechaDesde) >= 0) 
                            && (DateTime.Compare(x.FechaPedido, fechaHasta) <= 0))
                .OrderByDescending(x => x.FechaPedido)
                .ToListAsync(cancellationToken);
        }

        private async Task<List<Pedido>> ObtenerPedidosDeEmpresa(int idEmpresa, DateTime fechaDesde, DateTime fechaHasta, CancellationToken cancellationToken)
        {
            return await _context.Pedidos.Include(x => x.EmpresaMandante)
                .Where(x => x.EmpresaMandante.IdEmpresa == idEmpresa 
                            && (DateTime.Compare(x.FechaPedido, fechaDesde) >= 0)
                            && (DateTime.Compare(x.FechaPedido, fechaHasta) <= 0))
                .OrderByDescending(x => x.FechaPedido)
                .ToListAsync(cancellationToken);
        }
    }
}
