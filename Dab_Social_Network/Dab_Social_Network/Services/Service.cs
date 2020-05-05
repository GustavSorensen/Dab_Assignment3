using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    interface IService<Model>
    {
        List<Model> GetAll();
        Model GetSingle(string id);
        Model Add(Model entity);
        void Update(Model entity, string id);
        void Delete(string id);
    }
    public class Service<Model> : IService<Model> where Model : IModel
    {
        private readonly IMongoCollection<Model> entities;
        //private string name;
        public Service() { }
        public Service(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.UserCollectionName);
            var db = client.GetDatabase(settings.DatabaseName);

            entities = db.GetCollection<Model>(settings.UserCollectionName);
        }
        public List<Model> GetAll()
        {
            return entities.Find(e => true).ToList();
        }

        public Model GetSingle(string id)
        {
            return entities.Find(id).First();
        }

        public Model Add(Model entity)
        {
            entities.InsertOne(entity);
            return entity;
        }

        public void Update(Model entity, string id)
        {
            entities.ReplaceOne(x => x.Id == id, entity);
        }
        public void Delete(string id)
        {
            entities.DeleteOne(id);
        }
    }
}
