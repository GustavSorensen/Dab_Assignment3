using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Operations;

namespace Dab_Social_Network.Models
{
    public class User : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public List<string> PostIds { get; set; } = new List<string>();
        public List<string> CircleIds { get; set; } = new List<string>();
        public List<string> FollowerIds { get; set; } = new List<string>();
        public List<string> BlockedUserIds { get; set; } = new List<string>();
        
    }
}
