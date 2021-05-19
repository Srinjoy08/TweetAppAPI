using NUnit.Framework;
using TweetAppAPI.Controllers;
using TweetAppAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using TweetAppAPI.Test.Helper;
using TweetAppAPI.Services;
using TweetAppAPI.Models;

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

        [TestCase("ajaysharma2021","ajay2020")]
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
        }
        
        [Test]
        public void Register_Test_Fail()
        {
            var request = new User
            {
                FirstName = "Suresh",
                LastName = "Sen",
                Email = "sureshsen@gmail.com",
                LoginId = "ajaysharma2021",
                Password = "suresh2021",
                contactNumber = "2233445566"
            };
            var result = controller.Register(request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public void Register_Test_Fail_2()
        {
            var request = new User
            {
                FirstName = "Suresh",
                LastName = "Sen",
                Email = "ajay.sharma@xyzmail.com",
                LoginId = "sureshsen2021",
                Password = "suresh2021",
                contactNumber = "2233445566"
            };
            var result = controller.Register(request);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}