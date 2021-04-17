using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.Services
{
    public class TweetAppServices : ITweetAppServices
    {
        private IMongoCollection<User> _users;
        private IMongoCollection<Tweet> _tweets;
        public TweetAppServices(IMongoClient client)
        {
            var database = client.GetDatabase("TweetDB");
            _users = database.GetCollection<User>("User_Details");
            _tweets = database.GetCollection<Tweet>("Tweet");

        }

        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public User GetUserByLoginId(string loginId)
        {
            return _users.Find(user => user.LoginId == loginId).FirstOrDefault();
        }
        public User GetUserByEmailId(string email)
        {
            return _users.Find(user => user.Email == email).FirstOrDefault();
        }
        public User GetUserDetails(string loginId)
        {
            var result = _users.AsQueryable().Where(s => s.LoginId == loginId).Select(s => new { s.FirstName, s.LastName, s.Email }).FirstOrDefault();
            User user = new User();
            user.FirstName = result.FirstName;
            user.LastName = result.LastName;
            user.Email = result.Email;
            return user;
        }
        public int LoginUser(string loginId, string password)
        {
            User isExists = GetUserByLoginId(loginId);
            if (isExists != null)
            {
                if (isExists.Password == password)
                    return 0;
                else
                    return 2;
            }
            else
                return 1;
        }

        public int RegisterUser(User user)
        {
            User isExists = GetUserByLoginId(user.LoginId);
            if (isExists != null)
            {
                return 1;
            }
            isExists = GetUserByEmailId(user.Email);
            if (isExists != null)
            {
                return 2;
            }
            else
            {
                user.Id = Guid.NewGuid().ToString();
                _users.InsertOne(user);
                return 0;
            }

        }
        public List<Tweet> GetTweets()
        {
            return _tweets.Find(tweet => true).ToList();
        }
        public int PostTweet(Tweet tweet)
        {
            tweet.Id = Guid.NewGuid().ToString();
            var result = _users.Find(user => user.LoginId == tweet.LoginId).FirstOrDefault();
            if (result != null)
            {
                tweet.PostedBy = string.Format(result.FirstName + " " + result.LastName);
                _tweets.InsertOne(tweet);
                return 0;
            }
            else
                return 1;
        }

        public int PostReply(Reply reply)
        {
            reply.ReplyId = Guid.NewGuid().ToString();
            var result = _users.Find(user => user.LoginId == reply.ReplyLoginId).FirstOrDefault();
            if (result != null )
            {
                reply.RepliedBy = string.Format(result.FirstName + " " + result.LastName);

                var filter = Builders<Tweet>.Filter.Eq(e => e.Id, reply.TweetId);

                var update = Builders<Tweet>.Update.Push<Reply>(e => e.Replies, reply);

                _tweets.FindOneAndUpdate(filter, update);

                return 0;
            }
            else
                return 1;
        }
    }
}
