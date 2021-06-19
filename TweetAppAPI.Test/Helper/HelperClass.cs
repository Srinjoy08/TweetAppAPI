using MongoDB.Driver;
using TweetAppAPI.Controllers;
using TweetAppAPI.DbContext;
using TweetAppAPI.Repository;
using TweetAppAPI.Services;

namespace TweetAppAPI.Test.Helper
{
    public class HelperClass
    {
        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly IMongoDbContext _mongoDbContext;
        public HelperClass()
        {
            var MongoUri = "mongodb+srv://DBUser2021:dbuser2021@cluster0.lrjku.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(MongoUri);
            _mongoDbContext = new MongoDbContext(mongoClient);
            _userRepository = new UserRepository(_mongoDbContext, new EmailService());
            _tweetRepository = new TweetRepository(_mongoDbContext, _userRepository);
        }
         
        public TweetAppController GetTweetAppController()
        {
            return new TweetAppController(_userRepository, _tweetRepository);
        }
    }
}
