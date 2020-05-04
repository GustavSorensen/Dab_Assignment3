using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_Social_Network.Models
{
    public class Circle : Model
    {
        public string Name { get; set; }
        public IEnumerable<string> UserIds { get; set; } = new List<string>();
        public IEnumerable<string> PostIds { get; set; } = new List<string>();
    }
}
