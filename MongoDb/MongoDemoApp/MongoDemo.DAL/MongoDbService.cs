using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Common;
using MongoDemo.DAL.Models;

namespace MongoDemo.DAL
{
    public class MongoDbService
    {
        private readonly IMongoCollection<UserModel> _usersCollection;
        private readonly IMongoDatabase _db;
        public MongoDbService(IOptions<MongoDBSettings> mongoDBSettings) {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<UserModel>(mongoDBSettings.Value.UserColletion);
            _db = database;
        }

        public async Task<List<T>> GetAll<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            var result = await (await collection.FindAsync(new BsonDocument())).ToListAsync();

            return result;
        }

        public async Task InsertRecord<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }
    }
}
