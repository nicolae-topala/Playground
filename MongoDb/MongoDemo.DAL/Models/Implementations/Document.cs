using MongoDB.Bson;
using MongoDemo.DAL.Models.Interfaces;

namespace MongoDemo.DAL.Models.Implementations
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}
