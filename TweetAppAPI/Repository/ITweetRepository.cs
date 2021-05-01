using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.Repository
{
    public interface ITweetRepository
    {
        public List<Tweet> GetAllTweets();
        public int PostTweet(Tweet tweet);
        public int PostReply(Reply reply);
    }
}
