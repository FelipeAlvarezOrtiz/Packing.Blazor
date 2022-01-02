using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Packing.Core.Pedidos;

namespace Packing.Persistencia.Repositorios
{
    public class PedidosRepository
    {
        private const string PedidosCollectionName = "Pedidos";
        private readonly IMongoCollection<Pedido> _dbCollection;
        private readonly FilterDefinitionBuilder<Pedido> _filterBuilder = Builders<Pedido>.Filter;

        public PedidosRepository()
        {
            var mongoCliente = new MongoClient("mongodb://127.0.0.1:27017");
            var database = mongoCliente.GetDatabase("PackingBertolla");
            _dbCollection = database.GetCollection<Pedido>(PedidosCollectionName);
        }

        public async Task<IReadOnlyCollection<Pedido>> ObtenerTodosAsync()
        {
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<Pedido> ObtenerAsync(Guid idPedido)
        {
            var filter = _filterBuilder.Eq(pedido => pedido.GuidPedido,idPedido);
            return await _dbCollection.Find(filter).FirstAsync();
        }

        public async Task CrearPedido(Pedido pedidoRequest)
        {
            await _dbCollection.InsertOneAsync(pedidoRequest);
        }

        public async Task<List<Pedido>> ObtenerPedidosDeLaEmpresa(int idEmpresa)
        {
            var filter = _filterBuilder.Eq(pedido => pedido.EmpresaMandante.IdEmpresa, idEmpresa);
            return await _dbCollection.Find(filter).ToListAsync();
        }

        public async Task ActualizarPedido(Pedido pedidoRequest)
        {
            var filter = _filterBuilder.Eq(pedido => pedido.GuidPedido, pedidoRequest.GuidPedido);
            await _dbCollection.ReplaceOneAsync(filter, pedidoRequest);
        }

    }
}
