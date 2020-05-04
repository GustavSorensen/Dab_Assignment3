using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Operations;

namespace Dab_Social_Network.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }
        public string content { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        [BsonElement("ImageUrl")]
        [Display(nameof = "Photo")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
