using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.DbContext
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _database;
        public MongoDbContext(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("TweetDB");
        }

        public IMongoCollection<User> GetUserCollection()
        {
            return _database.GetCollection<User>("User_Details");
        }

        public IMongoCollection<Tweet> GetTweetCollection()
        {
            return _database.GetCollection<Tweet>("Tweet");
        }
    }
}
