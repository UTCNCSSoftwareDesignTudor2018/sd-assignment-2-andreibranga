using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Restaurant.Data.MongoDB
{
    public class MongoDbConnection
    {
        public IMongoDatabase Db { get; }
        public IMongoCollection<BsonDocument> Collection { get; }
        public MongoClient Client { get; }

        public MongoDbConnection(string database,string collection)
        {
           Client = new MongoClient();
            Db = Client.GetDatabase(database);
            this.Collection= Db.GetCollection<BsonDocument>(collection);
        }


    }
}
