using MongoDemo.Common.Interfaces;

namespace MongoDemo.Common
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
