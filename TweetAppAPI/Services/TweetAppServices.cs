﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.Services
{
    public class TweetAppServices : ITweetAppServices
    {
        private IMongoCollection<User> _users;
        public TweetAppServices(IMongoClient client)
        {
            var database = client.GetDatabase("TweetDB");
            _users = database.GetCollection<User>("User_Details");
        }

        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public User GetUserByLoginId(string loginId)
        {
            return _users.Find(user => user.LoginId == loginId).FirstOrDefault();
        }
        public User GetUserByEmailId(string email)
        {
            return _users.Find(user => user.Email == email).FirstOrDefault();
        }

        

        public int RegisterUser(User user)
        {
            User isExists = GetUserByLoginId(user.LoginId);
            if(isExists != null)
            {
                return 1;
            }
            isExists = GetUserByEmailId(user.Email);
            if(isExists != null)
            {
                return 2;
            }
            else
            {
                user.Id = Guid.NewGuid().ToString();
                _users.InsertOne(user);
                return 0;
            }
            
        }

    }
}
