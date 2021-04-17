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
        [Route("[controller]/{user}")]
        
        public IActionResult GetUserDetails(User user)
        {
            User response =_tweetAppServices.GetUserDetails(user.LoginId);
            return Ok(response);
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
