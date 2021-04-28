using System.Collections.Generic;
using TweetAppAPI.Models;

namespace TweetAppAPI.Repository
{
    public interface ITweetAppRepository
    {
        public User FetchUserDetails(string loginId);
        public int Login(string loginId, string password);
        public int Register(User user);
        public List<Tweet> GetAllTweets();
        public int PostTweet(Tweet tweet);
        public int PostReply(Reply reply);
        public string RequestOTP(string loginId);
        public int ResetPassword(string loginId, string password);
    }
}
