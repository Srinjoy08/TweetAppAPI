using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]/{user}")]
        public IActionResult RegisterUser(User user)
        {
            return Ok(_tweetAppServices.RegisterUser(user));
        }
    }
}
