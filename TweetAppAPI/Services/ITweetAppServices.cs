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
        public User GetUserDetails(string loginId);
        public User GetUserByEmailId(string email);
        public int LoginUser(string loginId, string password);
        public int RegisterUser(User user);
        
    }
}
