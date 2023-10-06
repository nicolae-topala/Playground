using MongoDB.Driver;
using MongoDemo.DAL.Models.Interfaces;
using System.Linq.Expressions;

namespace MongoDemo.DAL.Repositories.Implementations
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();
        IFindFluent<TDocument, TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);
        IFindFluent<TDocument, TDocument> FindAll();
        IFindFluent<TDocument, TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        IFindFluent<TDocument, TDocument> FindByIdAsync(string id);
        Task InsertOneAsync(TDocument document);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task ReplaceOneAsync(Expression<Func<TDocument, bool>> filterExpression, TDocument document);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteByIdAsync(string id);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}
