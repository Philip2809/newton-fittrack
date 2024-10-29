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
            // // Running
            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-3),
                Type = "Cardio day 1",
                Duration = TimeSpan.Parse("00:50:00"),
                CaloriesBurned = 800,
                Notes = "Ran 10 km 1/10"
            });

            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-2),
                Type = "Cardio day 2",
                Duration = TimeSpan.Parse("00:48:00"),
                CaloriesBurned = 800,
                Notes = "Ran 10 km 2/10"
            });

            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-1),
                Type = "Cardio day 3",
                Duration = TimeSpan.Parse("00:51:00"),
                CaloriesBurned = 800,
                Notes = "Ran 10 km 3/10"
            });

            // // Lifting
            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-3).AddHours(-5),
                Type = "Strength day 1",
                Duration = TimeSpan.Parse("00:33:00"),
                CaloriesBurned = 600,
                Notes = "Leg day (1/5)"
            });

            WorkoutManager.AddWorkout(user, new()
            {
                Date = DateTime.Now.AddDays(-1).AddHours(-5),
                Type = "Strength day 2",
                Duration = TimeSpan.Parse("00:28:00"),
                CaloriesBurned = 600,
                Notes = "Arms (2/5)"
            });
        }
    }
}
