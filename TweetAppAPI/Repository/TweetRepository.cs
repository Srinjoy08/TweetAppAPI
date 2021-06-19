using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.DbContext;
using TweetAppAPI.Models;

namespace TweetAppAPI.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IMongoCollection<Tweet> _tweets;
        private readonly IUserRepository _userRepository;
        public TweetRepository(IMongoDbContext dbContext, IUserRepository userRepository)
        {
            _tweets = dbContext.GetTweetCollection();
            _userRepository = userRepository;
        }

        public List<Tweet> GetAllTweets()
        {
            return _tweets.Find(tweet => true).SortByDescending(s => s.CreatedOn).ToList();
        }
        public int PostTweet(Tweet tweet)
        {
            tweet.Id = Guid.NewGuid().ToString();
            var result = _userRepository.GetUserByLoginId(tweet.LoginId);
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
            var result = _userRepository.GetUserByLoginId(reply.ReplyLoginId);
            if (result != null)
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
