using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.Services
{
    public interface ITweetAppServices
    {
        public List<User> GetAllUsers();
        public User GetUserByLoginId(string loginId);
        public bool RegisterUser(User user);
        
    }
}
