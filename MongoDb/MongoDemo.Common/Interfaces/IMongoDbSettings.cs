namespace MongoDemo.Common.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionURI { get; set; }
        string DatabaseName { get; set; }
    }
}
