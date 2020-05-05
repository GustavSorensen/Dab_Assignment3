using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver.Core.Operations;

namespace Dab_Social_Network.Models
{
    public class Post : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime TimeCreated { get; set; }

        [BsonElement("ImageUrl")]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
