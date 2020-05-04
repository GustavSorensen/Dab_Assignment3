using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dab_Social_Network.Models;

namespace Dab_Social_Network.Services
{
    public class CircleService
    {
        private readonly IMongoCollection<Circle> circles;

        public CircleService()
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("");

            circles = db.GetCollection<Circle>("Circle");
        }

        public List<Circle> GetAllCircles()
        {
            return circles.Find(c => true).ToList();
        }

        public Circle GetSingleCircle(string id)
        {
            return circles.Find(c => c.CircleId == id).First();
        }

        public Circle AddCircle(Circle circle)
        {
            circles.InsertOne(circle);
            return circle;
        }
    }
}
