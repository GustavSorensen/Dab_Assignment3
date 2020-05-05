using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dab_Social_Network.Models
{
    public interface IModel
    {
        string Id { get; set; }
    }
}
