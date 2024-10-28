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
            // Default users

            // // Default user
            User user = new()
            {
                Username = "user",
                Country = "Sweden",
                Password = "password",
                TwoFA = true,
                SecurityQuestion = "Vad är en banan?",
                SecurityAnswer = "ett bär"
            };


            // // Default admin
            User admin = new()
            {
                Username = "admin",
                Country = "Sweden",
                Password = "password",
                IsAdmin = true,
                SecurityQuestion = "Vad är en tomat?",
                SecurityAnswer = "en frukt"
            };

            UserManager.AddUser(user);
            UserManager.AddUser(admin);

            // Default workouts
            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-1),
                Type = "Cardio",
                Duration = TimeSpan.FromHours(1),
                CaloriesBurned = 600,
            });
        }
    }
}
