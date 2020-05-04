using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> posts;

        public PostService()
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("");

            posts = db.GetCollection<Post>("Post");

        }

        public List<Post> GetAllPosts()
        {
            return posts.Find(post => true).ToList();
        }

        public Post GetSinglePost(string id)
        {
            return posts.Find(post => post.PostId == id).FirstOrDefault();
        }

        public List<Post> GetPostByUser(string user)
        {
            return posts.Find(p => p.UserId == user).ToList();
        }

        public Post AddPost(Post post)
        {
            posts.InsertOne(post);
            return post;
        }

        public async void UpdatePost(Post post)
        {
            var list = new List<Post> { post };

            var filter = Builders<Post>.Filter.Eq("Id", post.PostId);
            var update = Builders<Post>.Update.Set("Post", list);
            await posts.UpdateOneAsync(filter, update);

        }
    }
}
