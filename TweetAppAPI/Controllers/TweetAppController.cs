using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;
using TweetAppAPI.Services;

namespace TweetAppAPI.Controllers
{
    [ApiController]

    public class TweetAppController : ControllerBase
    {
        private ITweetAppServices _tweetAppServices;
        public TweetAppController(ITweetAppServices tweetAppServices)
        {
            _tweetAppServices = tweetAppServices;
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetAllUsers()
        {
            return Ok(_tweetAppServices.GetAllUsers());
        }

        [HttpGet]
        [Route("[controller]/{loginId}")]

        public IActionResult GetUserDetails(string loginId)
        {
            User response = _tweetAppServices.GetUserDetails(loginId);
            return Ok(response);
        }

        [HttpGet]
        [Route("[controller]/tweets/")]
        public IActionResult GetTweets()
        {
            return Ok(_tweetAppServices.GetTweets());
        }

        [HttpPost]
        [Route("[controller]/tweets/{tweet}")]
        public IActionResult PostTweet(Tweet tweet)
        {
            int result = _tweetAppServices.PostTweet(tweet);
            if (result == 1)
            {
                return BadRequest("Failed to Post Tweet..!!");
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("[controller]/replies/{reply}")]
        public IActionResult PostReply(Reply reply)
        {
            int result = _tweetAppServices.PostReply(reply);
            if (result == 1)
            {
                return BadRequest("Failed to Reply to Tweet..!!");
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("[controller]/login/{user}")]
        public IActionResult LoginUser(User user)
        {
            int result = _tweetAppServices.LoginUser(user.LoginId, user.Password);
            ArrayList l =new ArrayList();
            l.Add(user.LoginId);
            if(result == 1)
            {
                return Unauthorized("Login Id does not exists..!!");
            }
            else if(result == 2)
            {
                return Unauthorized("Password Incorrect..!!");
            }
            else
            {
                return Ok(l);
            }
        }

        [HttpPost]
        [Route("[controller]/register/{user}")]
        public IActionResult RegisterUser(User user)
        {
            int result = _tweetAppServices.RegisterUser(user);
            
            if(result == 1)
            {
                return BadRequest("Login Id already exists..!!");
            }
            else if (result == 2)
            {
                return BadRequest("Email address already exists..!!");
            }
            else
            {
                return Ok();
            }
        }

        
    }
}
