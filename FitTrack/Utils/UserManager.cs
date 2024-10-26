using FitTrack.Model;
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
                users.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static User? AuthenticateUser(string username, string password)
        {
            return users.Find(u => u.Username == username && u.Password == password);
        }

    }
}
