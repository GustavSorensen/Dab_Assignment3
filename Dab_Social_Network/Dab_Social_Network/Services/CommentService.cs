using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Models;

namespace DabMongoDB.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> comments;

        public CommentService()
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("");

            comments = db.GetCollection<Comment>("Comment");
        }

        public List<Comment> ListComments()
        {
            return comments.Find(comment => true).ToList();
        }

        public Comment GetCommentFromId(string id)
        {
            return comments.Find(comment => comment.CommentId == id).FirstOrDefault();
        }

        public Comment NewComment(Comment comment)
        {
            comments.InsertOne(comment);
            return comment;
        }

        public async void UpdateComment(Comment comment)
        {
            var list = new List<Comment> { comment };

            var filter = Builders<Comment>.Filter.Eq("Id", comment.CommentId);
            var update = Builders<Comment>.Update.Set("Comment", list);

            await comments.UpdateOneAsync(filter, update);

        }
    }
}
