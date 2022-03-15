using Lib.Service.Mongo.Context;
using Lib.Service.Mongo.Entities;
using Lib.Service.Mongo.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Service.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        public readonly IMongoCollection<TDocument> collection;
        public readonly MongoClient client;

    private protected static string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        /// <summary>
        /// Constructor para inicializar el servicio a través del startup
        /// </summary>
        /// <param name="options"></param>
        public MongoRepository(IOptions<MongoContext> options)
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

            client = new MongoClient(options.Value.ConnectionString);

            var db = client.GetDatabase(options.Value.Database);

            var collectionName = GetCollectionName(typeof(TDocument));

            collection = db.GetCollection<TDocument>(collectionName);
        }

        /// <summary>
        /// Function to get single document by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TDocument> GetByFilter(FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default)
        {
            return await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Function to get all documents in a collection
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TDocument>> GetAll(CancellationToken cancellationToken = default)
        {
            return await collection.Find(Builders<TDocument>.Filter.Empty).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Functino to get single document by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TDocument> GetById(string Id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);

            return await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Function to update and replace a document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task UpdateReplaceDocument(TDocument document, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await collection.FindOneAndReplaceAsync(filter, document);
        }

        /// <summary>
        /// Function to insert one document document 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task InsertDocument(TDocument document, CancellationToken cancellationToken = default)
        {
            await collection.InsertOneAsync(document);
        }

        /// <summary>
        /// Function to delete document by id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteById(string Id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            await collection.FindOneAndDeleteAsync(filter);
        }

        /// <summary>
        /// Function to get a list of documents using pagination
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginationEntity<TDocument>> GetByPagination(PaginationEntity<TDocument> pagination, FilterDefinition<TDocument> filter, CancellationToken cancellationToken = default)
        {

            var resp = await collection
                        .Find(filter)
                        .Skip((pagination.page - 1) * pagination.pageSize)
                        .Limit(pagination.pageSize)
                        .ToListAsync(cancellationToken: cancellationToken);

            var totalDocuments = (await collection.CountDocumentsAsync(filter));

            var rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.pageSize));

            var totalPages = Convert.ToInt32(rounded);

            pagination.totalPages = totalPages;
            pagination.totalRows = Convert.ToInt32(totalDocuments);
            pagination.data = resp;

            return pagination;
        }

        public async Task<PaginationEntity<TDocument>> GetPagingInfo(PaginationEntity<TDocument> pagination, FilterDefinition<TDocument> filter)
        {

            var totalDocuments = (await collection.CountDocumentsAsync(filter));

            var rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.pageSize));

            var totalPages = Convert.ToInt32(rounded);

            pagination.totalPages = totalPages;
            pagination.totalRows = Convert.ToInt32(totalDocuments);

            return pagination;
        }
    }
}
