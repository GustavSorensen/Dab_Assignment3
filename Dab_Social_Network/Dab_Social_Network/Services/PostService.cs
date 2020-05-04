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
        private readonly IMongoCollection<Post> posts;

        public PostService()
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("");

            posts = db.GetCollection<Post>("Post");
        }
        public List<Post> GetPostByUserId(string id)
        {
            return posts.Find(p => p.UserId == id).ToList();
        }
    }
}
