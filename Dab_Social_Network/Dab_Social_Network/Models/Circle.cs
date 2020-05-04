using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_Social_Network.Models
{
    public class Circle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircleId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> UserIds { get; set; }
        public IEnumerable<string> PostIds { get; set; }
    }
}
