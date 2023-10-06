using MongoDB.Bson.Serialization.Attributes;
using MongoDemo.Common.Attributes;

namespace MongoDemo.DAL.Models.Implementations
{
    [BsonCollection("Users")]
    public class UserModel : Document
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [BsonElement("dob")]
        public DateOnly DateOfBirth { get; set; }
    }
}
