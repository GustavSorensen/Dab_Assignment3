using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Models;
using MongoDB.Driver;

namespace Dab_Social_Network.Services
{
    public class CommentService : Service<Comment>
    {
        private readonly IMongoCollection<Comment> entities;
        public CommentService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            entities = db.GetCollection<Comment>(settings.commentCollectionName);
        }

    }
}
