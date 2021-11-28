using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Reportes;
using Packing.Persistencia;
using Packing.Shared.ReportesDto;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Packing.Negocio.Reportes
{
    public class ReporteJefePackingEntreFechas : IRequestHandler<ReporteJefePackingQuery,ReporteJefePacking>
    {
        private readonly ApplicationDbContext _context;

        public ReporteJefePackingEntreFechas(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReporteJefePacking> Handle(ReporteJefePackingQuery request, CancellationToken cancellationToken)
        {
            var reporte = new ReporteJefePacking();
            var pedidosEnRango = await _context.Pedidos.Where(pedido =>
                    DateTime.Compare(pedido.FechaPedido, request.FechaHasta) <= 1
                        && DateTime.Compare(pedido.FechaPedido, request.FechaDesde) >= 0)
                        .Select(pedido => pedido.GuidPedido).ToListAsync(cancellationToken: cancellationToken);

            var pedidos = await _context.Pedidos.Where(pedido =>
                            DateTime.Compare(pedido.FechaPedido, request.FechaHasta) <= 1
                                && DateTime.Compare(pedido.FechaPedido, request.FechaDesde) >= 0)
                                .ToListAsync(cancellationToken: cancellationToken);
           
            var detallesEnRango = await _context.DetallePedidos.Include(detalle => detalle.Estado)
                                                               .Where(detalle => pedidosEnRango.Contains(detalle.PedidoCabecera.GuidPedido))
                                                               .Where(detalle => detalle.Estado.IdEstadoPedido == 1)
                                                               .Include(detalle => detalle.ProductoInterno)
                                                               .ThenInclude(producto => producto.Grupo)
                                                               .ToListAsync(cancellationToken);
            // EN FOREACH IR POR LOS DETALLES POR CADA PEDIDO PEDIDO QUE TENGAN EL ESTADO == 1
            // SE GUARDA LA EMPRESA DE LA CABECERA Y SE INGRESA EL PEDIDO AL DICCIONARIO
            // LUEGO PRODUCTO POR PRODUCTO QUE NO HAYA SIDO PROCESADO 
            foreach (var pedido in pedidos)
            {

            }
            throw new System.NotImplementedException();
        }
    }
}
