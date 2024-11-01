using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    class CardioWorkout : Workout
    {
        public override string Type { get { return "Cardio"; } }

        public int Distance { get; set; }
    }
}
