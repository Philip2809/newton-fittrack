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
            // Check if username already exists
            var existingUser = users.Find(u => u.Username == user.Username);
            if (existingUser == null)
            {
                user.Id = Guid.NewGuid();
                users.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static User? UpdateUser(User user, string newUsername, string newCountry, string newPassword, bool twoFAInput)
        {
            // If user wants a new username check that its not used
            if (user.Username != newUsername) {
                // if used return false
                if (users.Find(u => u.Username ==  newUsername) != null) return null;
            }

            // If all good then update the existing user
            var existingUser = users.Find(u => u.Username == user.Username);
            if (existingUser == null) return null;
            existingUser.Username = newUsername;
            existingUser.Country = newCountry;
            existingUser.Password = newPassword;
            existingUser.TwoFA = twoFAInput;
            return existingUser;
        }

        public static User? AuthenticateUser(string username, string password)
        {
            return users.Find(u => u.Username == username && u.Password == password);
        }
    }
}
