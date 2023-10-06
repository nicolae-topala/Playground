using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Common.Attributes;
using MongoDemo.Common.Interfaces;
using MongoDemo.DAL.Models.Interfaces;
using MongoDemo.DAL.Repositories.Implementations;
using System.Linq.Expressions;

namespace MongoDemo.DAL.Repositories.Interfaces
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionURI).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private static string? GetCollectionName(Type documentType)
        {
            var collection = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute;
            var collectionName = collection?.CollectionName;

            return collectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable() =>
            _collection.AsQueryable();

        public virtual IFindFluent<TDocument, TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression) =>
            _collection.Find(filterExpression);

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression) =>
            _collection.Find(filterExpression)
                .Project(projectionExpression)
                .ToEnumerable();

        public virtual IFindFluent<TDocument, TDocument> FindAll() =>
            _collection.Find(_ => true);

        public virtual IFindFluent<TDocument, TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression) =>
            _collection.Find(filterExpression);

        public virtual IFindFluent<TDocument, TDocument> FindByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            var result = _collection.Find(filter);

            return result;
        }

        public virtual async Task InsertOneAsync(TDocument document) =>
            await _collection.InsertOneAsync(document);

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents) =>
            await _collection.InsertManyAsync(documents);

        public virtual async Task ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression, TDocument document) =>
            await _collection.ReplaceOneAsync(filterExpression, document);

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression) =>
            await _collection.FindOneAndDeleteAsync(filterExpression);

        public async Task DeleteByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression) =>
            await _collection.DeleteManyAsync(filterExpression);
    }
}
