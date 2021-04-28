using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TweetAppAPI.Models;
using TweetAppAPI.Repository;

namespace TweetAppAPI.Controllers
{
    [ApiController]

    public class TweetAppController : ControllerBase
    {
        private readonly ITweetAppRepository _tweetAppRepository;

        public TweetAppController(ITweetAppRepository tweetAppRepository)
        {
            _tweetAppRepository = tweetAppRepository;
        }

        [HttpPost]
        [Route("[controller]/login/{user}")]
        public IActionResult Login(User user)
        {
            var response = _tweetAppRepository.Login(user.LoginId, user.Password);            
            if (response == 1)
            {
                return Unauthorized("Login Id Incorrect..!!");
            }
            else if (response == 2)
            {
                return Unauthorized("Password Incorrect..!!");
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
            var response = _tweetAppRepository.Register(user);

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
            var response = _tweetAppRepository.FetchUserDetails(loginId);

            if(response != null)
                return Ok(response);
            else
                return Unauthorized("Login Id does not exists..!!");
        }

        [HttpPost]
        [Route("[controller]/requestOTP/{user}")]
        public IActionResult RequestOTP(User user)
        {
            var response = _tweetAppRepository.RequestOTP(user.LoginId);

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
            var response = _tweetAppRepository.ResetPassword(user.LoginId, user.Password);
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
            return Ok(_tweetAppRepository.GetAllTweets());
        }

        [HttpPost]
        [Route("[controller]/tweets/{tweet}")]
        public IActionResult PostTweet(Tweet tweet)
        {
            var response = _tweetAppRepository.PostTweet(tweet);
            if (response == 1)
            {
                return BadRequest("Failed to Post Tweet..!!");
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("[controller]/postReply/{reply}")]
        public IActionResult PostReply(Reply reply)
        {
            var response = _tweetAppRepository.PostReply(reply);
            if (response == 1)
            {
                return BadRequest("Failed to Reply..!!");
            }
            else
            {
                return Ok();
            }
        }

        
        
    }
}
