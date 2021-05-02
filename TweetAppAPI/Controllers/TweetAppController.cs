using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using TweetAppAPI.Models;
using TweetAppAPI.Repository;

namespace TweetAppAPI.Controllers
{
    [ApiController]

    public class TweetAppController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly ProducerConfig config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        private readonly string topic = "Tweet-Topic";

        public TweetAppController(IUserRepository userRepository, ITweetRepository tweetRepository)
        {
            _userRepository = userRepository;
            _tweetRepository = tweetRepository;
        }
        
        [HttpPost]
        [Route("[controller]/login/{user}")]
        public IActionResult Login(User user)
        {
            var response = _userRepository.Login(user.LoginId, user.Password);            
            if (response == 1)
            {
                return Unauthorized("Login Id is incorrect..!!");
            }
            else if (response == 2)
            {
                return Unauthorized("Password is incorrect..!!");
            }
            else
            {
                ArrayList list = new ArrayList();
                list.Add(user.LoginId);
                return Ok(list);
            }
        }

        [HttpPost]
        [Route("[controller]/register/{user}")]
        public IActionResult Register(User user)
        {
            var response = _userRepository.Register(user);

            if (response == 1)
            {
                return BadRequest("Login Id already exists..!!");
            }
            else if (response == 2)
            {
                return BadRequest("Email address already exists..!!");
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("[controller]/fetchUserDetails/{loginId}")]
        public IActionResult FetchUserDetails(string loginId)
        {
            var response = _userRepository.FetchUserDetails(loginId);

            if(response != null)
                return Ok(response);
            else
                return Unauthorized("Login Id does not exists..!!");
        }

        [HttpPost]
        [Route("[controller]/requestOTP/{user}")]
        public IActionResult RequestOTP(User user)
        {
            var response = _userRepository.RequestOTP(user.LoginId);

            if(response != null)
            {
                return Ok(response);
            }
            else
                return Unauthorized("Login Id does not exists..!!");
        }

        [HttpPost]
        [Route("[controller]/resetPassword/{user}")]
        public IActionResult ResetPassword(User user)
        {
            var response = _userRepository.ResetPassword(user.LoginId, user.Password);
            if (response == 0)
            {
                return Ok();
            }
            else
                return BadRequest("Failed to Reset Password..!!");
        }

        [HttpGet]
        [Route("[controller]/tweets/")]
        public IActionResult GetAllTweets()
        {
            return Ok(_tweetRepository.GetAllTweets());
        }

        [HttpPost]
        [Route("[controller]/tweets/{tweet}")]
        public IActionResult PostTweet(Tweet tweet)
        {
            var response = _tweetRepository.PostTweet(tweet);
            if (response == 1)
            {
                return BadRequest("Failed to Post Tweet..!!");
            }
            else
            {
                SendToKafka(topic, tweet.Body);
                return Ok();
            }
        }

        [HttpPost]
        [Route("[controller]/postReply/{reply}")]
        public IActionResult PostReply(Reply reply)
        {
            var response = _tweetRepository.PostReply(reply);
            if (response == 1)
            {
                return BadRequest("Failed to Reply..!!");
            }
            else
            {
                return Ok();
            }
        }

        private Object SendToKafka(string topic, string message)
        {
            using (var producer =
                 new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<Null, string> { Value = message })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
            return null;
        }

    }
}
