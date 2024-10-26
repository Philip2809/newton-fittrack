using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    class WorkoutsWindowViewModel : ViewModelBase
    {
        public RelayCommand<Window> SignOutCommand => new RelayCommand<Window>(window => SignOut(window));
        public RelayCommand<Window> UserInfoCommand => new RelayCommand<Window>(window => UserInfo(window));
        public RelayCommand<object> InfoCommand => new RelayCommand<object>(paramter => Info());

        public User User { get; set; }
        public WorkoutsWindowViewModel(User user)
        {
            User = user;
        }

        public List<Workout> Workouts
        {
            get { return WorkoutManager.GetWorkoutsByUser(User); }
        }

        private void SignOut(Window window)
        {
            new MainWindow().Show();
            window?.Close();
        }

        private void UserInfo(Window window)
        {
            new UserDetailsWindow(User).Show();
            window?.Close();
        }

        private void Info()
        {
            MessageBox.Show("We are a fittness tracking", "About us", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
