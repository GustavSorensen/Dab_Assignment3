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
        public UserService(ISocialNetworkDatabaseSettings settings) : base(settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            base.entities = db.GetCollection<User>(settings.UserCollectionName);
        }
    }
}
