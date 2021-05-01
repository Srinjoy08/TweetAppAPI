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
        public void Login_PassTest(string loginId, string password)
        {
            var result = controller.Login(new Models.User() { LoginId = loginId, Password = password});
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [TestCase("userTest2021", "user2021")]
        public void Login_FailTest(string loginId, string password)
        {
            var result = controller.Login(new Models.User() { LoginId = loginId, Password = password });
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
        }

        [TestCase("ajaysharma2021")]
        public void FetchUserDetails_PassTest(string loginId)
        {
            var result = controller.FetchUserDetails(loginId);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
              

        [TestCase("userTest2021")]
        public void FetchUserDetails_FailTest(string loginId)
        {
            var result = controller.FetchUserDetails(loginId);
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
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