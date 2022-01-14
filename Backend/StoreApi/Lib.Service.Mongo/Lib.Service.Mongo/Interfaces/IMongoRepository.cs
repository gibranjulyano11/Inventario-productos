using Lib.Service.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Service.Mongo.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll(CancellationToken cancellationToken = default);

        Task<TDocument> GetByFilter(FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default);

        Task<TDocument> GetById(string Id, CancellationToken cancellationToken = default);

        Task UpdateReplaceDocument(TDocument document, CancellationToken cancellationToken = default);

        Task InsertDocument(TDocument document, CancellationToken cancellationToken = default);

        Task DeleteById(string Id, CancellationToken cancellationToken = default);

        Task<PaginationEntity<TDocument>> GetByPagination(PaginationEntity<TDocument> pagination, FilterDefinition<TDocument> filter, CancellationToken cancellationToken);

    }
}
