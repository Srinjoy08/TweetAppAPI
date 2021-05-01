using MongoDB.Driver;
using TweetAppAPI.Controllers;
using TweetAppAPI.Repository;
using TweetAppAPI.Services;

namespace TweetAppAPI.Test.Helper
{
    public class HelperClass
    {
        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        public HelperClass()
        {
            var MongoUri = "mongodb+srv://DBUser2021:dbuser2021@cluster0.lrjku.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(MongoUri);
            _userRepository = new UserRepository(mongoClient, new EmailService());
            _tweetRepository = new TweetRepository(mongoClient, _userRepository);
        }
         
        public TweetAppController GetTweetAppController()
        {
            return new TweetAppController(_userRepository, _tweetRepository);
        }
    }
}
