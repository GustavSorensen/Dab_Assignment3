using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> users;

        public UserService()
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("");
            users = db.GetCollection<User>("User");
        }

        public List<User> GetUsers()
        {
            return users.Find(u => true).ToList();
        }
        public User GetSingleUser(string id)
        {
            return users.Find(u => u.UserId == id).First();
        }
        public User AddUser(User user)
        {
            users.InsertOne(user);
            return user;
        }
    }
}
