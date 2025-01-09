using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace PersonalExpenseTracker.Data 
{
    public class MongoDBContext {
        
        private readonly IMongoDatabase _database;

        public MongoDBContext(String connectionString, String databaseName){
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T> (String collectionName){
            return _database.GetCollection<T>(collectionName);
        }
    }
}