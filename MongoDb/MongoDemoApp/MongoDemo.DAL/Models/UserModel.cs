using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDemo.DAL.Models
{
    public class UserModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [BsonElement("dob")]
        public DateOnly DateOfBirth { get; set; }
    }
}
