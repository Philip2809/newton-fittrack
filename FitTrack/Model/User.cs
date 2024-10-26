using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class User : Person
    {
        // So that a workout manager can easily access the user's workouts
        public int Id { get; set; }

        // IsAdmin is set here to UserManager and WorkoutManager can use it easily
        public bool IsAdmin { get; set; } = false;
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public override void SignIn()
        {
            new WorkoutsWindow(this).Show();
        }
    }
}
