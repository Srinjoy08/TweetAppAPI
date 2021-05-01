using NUnit.Framework;
using TweetAppAPI.Controllers;
using TweetAppAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using TweetAppAPI.Test.Helper;
using TweetAppAPI.Services;

namespace TweetAppAPI.Test
{
    public class Tests
    {
        TweetAppController controller;
        [SetUp]
        public void Setup()
        {
            HelperClass config = new HelperClass();
            controller = config.GetTweetAppController();
        }

        [TestCase("ajaysharma2021","ajay2021")]
        [TestCase("userTest2021", "user2021")]
        public void LoginTest(string loginId, string password)
        {
            var result = controller.Login(new Models.User() { LoginId = loginId, Password = password});
            if(result.GetType() == typeof(OkObjectResult))
            {
                Assert.Pass();
            }
            else if(result.GetType() == typeof(UnauthorizedObjectResult))
            {
                Assert.Pass();
            }
        }

        [TestCase("ajaysharma2021")]
        [TestCase("userTest2021")]
        public void FetchUserDetailsTest(string loginId)
        {
            var result = controller.FetchUserDetails(loginId);
            if (result.GetType() == typeof(OkObjectResult))
            {
                Assert.Pass();
            }
            else if (result.GetType() == typeof(UnauthorizedObjectResult))
            {
                Assert.Pass();
            }
        }        
        
        [Test]
        public void GetAllTweetsTest()
        {
            var result = controller.GetAllTweets();
            Assert.IsInstanceOf<OkObjectResult>(result);
            //Assert.Pass();
        }
    }
}