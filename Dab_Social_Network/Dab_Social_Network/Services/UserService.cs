using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Models;
using MongoDB.Driver;

namespace Dab_Social_Network.Services
{
    public class UserService : Service<User>
    {
        private readonly IMongoCollection<User> entities;
        public UserService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            entities = db.GetCollection<User>(settings.UserCollectionName);
        }
    }
}
