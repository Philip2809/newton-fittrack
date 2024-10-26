using FitTrack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Utils
{
    class WorkoutManager
    {
        public static List<Workout> workouts = new();

        public static void AddWorkout(User user, Workout workout)
        {
            workout.UserId = user.Id;
            workouts.Add(workout);
        }

        public static List<Workout> GetWorkoutsByUser(User user)
        {
            if (user.IsAdmin) return workouts;
            return workouts.FindAll(w => w.UserId == user.Id);
        }
    }
}
