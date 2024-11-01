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
            workout.Id = Guid.NewGuid();
            workout.UserId = user.Id;
            workouts.Add(workout);
        }

        public static void RemoveWorkout(Workout workout)
        {
            workouts.Remove(workout);
        }

        public static bool UpdateWorkout(Workout existingWorkout, Workout newWorkout)
        {
            if (existingWorkout.Type == newWorkout.Type)
            {
                // Type if the same, get list referance and update the values!
                var dbRef = workouts.Where(w => w.Id == existingWorkout.Id).ToList().First();
                if (dbRef == null) return false;

                dbRef.Date = newWorkout.Date;
                dbRef.CaloriesBurned = newWorkout.CaloriesBurned;
                dbRef.Duration = newWorkout.Duration;
                dbRef.Notes = newWorkout.Notes;

                if (dbRef is StrengthWorkout) ((StrengthWorkout)dbRef).Repetitions = ((StrengthWorkout)newWorkout).Repetitions;
                else if (dbRef is CardioWorkout) ((CardioWorkout)dbRef).Distance = ((CardioWorkout)newWorkout).Distance;
            } else
            {
                // Type is not the same, we need to delete the old one and create a new one with correct type!
                RemoveWorkout(existingWorkout);

                newWorkout.Id = Guid.NewGuid();
                // Keep the original userid so if admin edits the user keeps the workout
                newWorkout.UserId = existingWorkout.UserId;
                workouts.Add(newWorkout);
            }

            return true;
        }

        public static ObservableCollection<Workout> GetWorkoutsByUser(User user)
        {
            if (user is AdminUser) return workouts;
            return new ObservableCollection<Workout>(workouts.Where(w => w.UserId == user.Id));
        }
    }
}
