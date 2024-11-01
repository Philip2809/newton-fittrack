using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public enum WorkoutType
    {
        Strength,
        Cardio
    };
    public abstract class Workout
    {
        // UserId to allow workout manager to filter by user
        public Guid UserId { get; set; }
        // To easily allow delete and updates
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public abstract string Type { get; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        // Calories bruned are inputed by user as per requirements
    }
}
