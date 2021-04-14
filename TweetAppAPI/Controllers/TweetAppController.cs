using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("[controller]/{id}")]
        public IActionResult GetUserByLoginId(string id)
        {
            return Ok(_tweetAppServices.GetUserByLoginId(id));
        }
    }
}
