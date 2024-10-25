﻿using FitTrack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Utils
{
    class UserManager
    {
        public static List<User> users = new();

        public static bool AddUser(User user)
        {
            var existingUser = users.Find(u => u.Username == user.Username);
            if (existingUser == null)
            {
                user.Id = users.Count;
                users.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static User? UpdateUser(User user, string newUsername, string newCountry, string newPassword)
        {
            // If user wants a new username check that its not used
            if (user.Username != newUsername) {
                // if used return false
                if (users.Find(u => u.Username ==  newUsername) != null) return null;
            }

            // If all good then update the existing user
            var existingUser = users.Find(u => u.Username == user.Username);
            existingUser.Username = newUsername;
            existingUser.Country = newCountry;
            existingUser.Password = newPassword;
            return existingUser;
        }

        public static User? AuthenticateUser(string username, string password)
        {
            return users.Find(u => u.Username == username && u.Password == password);
        }

    }
}
