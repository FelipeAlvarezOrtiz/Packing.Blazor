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
    public class ReporteCosechaEntreFechas : IRequestHandler<ReporteCosecheroQuery,ReporteCosechero>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public ReporteCosechaEntreFechas(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ReporteCosechero> Handle(ReporteCosecheroQuery request, CancellationToken cancellationToken)
        {
            var reporteCosechero = new ReporteCosechero();
            var pedidosEnRango = await _context.Pedidos.Where(pedido => 
                    DateTime.Compare(pedido.FechaPedido,request.FechaHasta) <= 1 
                        && DateTime.Compare(pedido.FechaPedido,request.FechaDesde) >= 0)
                .Select(pedido => pedido.GuidPedido).ToListAsync(cancellationToken: cancellationToken);
            var detallesEnRango = await _context.DetallePedidos.Include(detalle => detalle.Estado)
                                                               .Where(detalle => pedidosEnRango.Contains(detalle.PedidoCabecera.GuidPedido))
                                                               .Where(detalle => detalle.Estado.IdEstadoPedido == 1)
                                                               .Include(detalle => detalle.ProductoInterno)
                                                               .ThenInclude(producto => producto.Grupo)
                                                               .ToListAsync(cancellationToken);
            foreach(var detalle in detallesEnRango)
            {
                if (reporteCosechero.Grupos.ContainsKey(detalle.ProductoInterno.Grupo.IdGrupo))
                {
                    var cantidadAnterior = reporteCosechero.Grupos[detalle.ProductoInterno.Grupo.IdGrupo].Item2;
                    var nuevaCantidad = (int)(cantidadAnterior + detalle.CantidadTotales);
                    reporteCosechero.Grupos[detalle.ProductoInterno.Grupo.IdGrupo] = Tuple.Create(detalle.ProductoInterno.Grupo, nuevaCantidad);
                }
                else
                {
                    reporteCosechero.Grupos.Add(detalle.ProductoInterno.Grupo.IdGrupo,Tuple.Create(detalle.ProductoInterno.Grupo,(int)detalle.CantidadTotales));
                }
            }
            reporteCosechero.FechaDeReporte = DateTime.Now;
            reporteCosechero.RangoFechasConsideradas = Tuple.Create(request.FechaDesde,request.FechaHasta);
            reporteCosechero.UsuarioSolicitante = request.UsuarioSolicitante;
            return reporteCosechero;
        }

    }
}
