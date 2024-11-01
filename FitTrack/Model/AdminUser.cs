using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    class AdminUser : User
    {
        // AdminUser type is checked in the WorkoutManager. I did not implement the ManageAllWorkouts here as that would create unneccesery code for the workoutswindow
        // to use different code based on the usertype, instead I check the usertype once in the workout manager
    }
}
