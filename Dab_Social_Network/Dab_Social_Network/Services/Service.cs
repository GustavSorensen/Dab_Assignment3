using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    interface IService<TEntity>
    {
        List<TEntity> Get();
        TEntity Get(string id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity, string id);
        void Delete(string id);
    }
    public class Service<TEntity> : IService<TEntity> where TEntity : IModel
    {
        private IMongoCollection<TEntity> entities;
        public IMongoCollection<TEntity> Entities { get { return entities; } set { entities = value; } }
        public List<TEntity> Get()
        {
            return entities.Find(e => true).ToList();
        }

        public TEntity Get(string id)
        {
            return entities.Find<TEntity>(x => x.Id == id).First();
        }

        public TEntity Add(TEntity entity)
        {
            entities.InsertOne(entity);
            return entity;
        }

        public void Update(TEntity entity, string id)
        {
            entities.ReplaceOne(x => x.Id == id, entity);
        }
        public void Delete(string id)
        {
            entities.DeleteOne(x => x.Id == id);
        }
    }
}
