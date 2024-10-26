using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }

        abstract public void SignIn();
    }
}
