using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    class WorkoutsWindowViewModel : ViewModelBase
    {
        public User User { get; set; }
        public WorkoutsWindowViewModel(User user)
        {
            User = user;
            Console.WriteLine(User.Username);
        }


        


    }
}
