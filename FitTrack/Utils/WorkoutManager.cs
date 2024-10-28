using FitTrack.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Utils
{
    class WorkoutManager
    {
        public static ObservableCollection<Workout> workouts = new();

        public static void AddWorkout(User user, Workout workout)
        {
            workout.UserId = user.Id;
            workouts.Add(workout);
        }

        public static void RemoveWorkout(Workout workout)
        {
            workouts.Remove(workout);
        }

        public static ObservableCollection<Workout> GetWorkoutsByUser(User user)
        {
            if (user.IsAdmin) return workouts;
            return new ObservableCollection<Workout>(workouts.Where(w => w.UserId == user.Id));
        }
    }
}
