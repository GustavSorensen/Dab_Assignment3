using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    public class PostService : Service<Post>, IService<Post>
    {
        public PostService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            Entities = db.GetCollection<Post>(settings.PostCollectionName);
        }
        public List<Post> GetPostByUserId(string id)
        {
            return Entities.Find(p => p.UserId == id).ToList();
        }
    }
}
