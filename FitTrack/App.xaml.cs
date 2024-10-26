using FitTrack.Model;
using FitTrack.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Console.WriteLine("Hello!");
            User user = new()
            {
                Username = "user",
                Country = "Sweden",
                Password = "password",
                SecurityQuestion = "",
                SecurityAnswer = ""
            };

            UserManager.AddUser(user);
        }
    }
}
