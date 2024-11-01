using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public override string Type { get { return "Strength"; } }
        public int Repetitions { get; set; }
    }
}
