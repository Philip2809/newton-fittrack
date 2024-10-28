using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public RelayCommand<Window> DetailsCommand => new RelayCommand<Window>(window => Details(window));
        public RelayCommand<object> RemoveCommand => new RelayCommand<object>(paramter => Remove());
        public RelayCommand<object> InfoCommand => new RelayCommand<object>(paramter => Info());
        public RelayCommand<object> AddWorkoutCommand => new RelayCommand<object>(paramter => AddWorkout());

        public User User { get; set; }
        public ObservableCollection<Workout> Workouts { get; private set; }

        private Workout? selectedWorkout = null;
        public Workout? SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }
        public WorkoutsWindowViewModel(User user)
        {
            User = user;
            Workouts = WorkoutManager.GetWorkoutsByUser(User);
            // For automatic updates of the DataGrid
            WorkoutManager.workouts.CollectionChanged += OnWorkoutsCollectionChanged;
        }

        private void OnWorkoutsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Workouts = new ObservableCollection<Workout>(WorkoutManager.GetWorkoutsByUser(User));
            OnPropertyChanged(nameof(Workouts));
        }

        private void SignOut(Window window)
        {
            new MainWindow().Show();
            CloseWindow(window);
        }

        private void Remove()
        {
            if (selectedWorkout == null)
            {
                Helpers.Error("Please select a workout to remove");
                return;
            }

            WorkoutManager.RemoveWorkout(selectedWorkout);
        }

        private void UserInfo(Window window)
        {
            new UserDetailsWindow(User).Show();
            CloseWindow(window);
        }

        private void AddWorkout()
        {
            new AddWorkoutWindow(User, null).Show();
        }

        private void Details(Window window)
        {
            if (selectedWorkout == null)
            {
                Helpers.Error("Please select a workout to view the details on");
                return;
            }

            new WorkoutDetailsWindow(User, selectedWorkout).Show();
            CloseWindow(window);
        }

        // Becase of the listener use this function to close instead of having to repeat the code
        private void CloseWindow(Window? window)
        {
            window?.Close();
            // Dispose of the listener when the window is closed
            WorkoutManager.workouts.CollectionChanged -= OnWorkoutsCollectionChanged;
        }

        private void Info()
        {
            MessageBox.Show("We are a fittness tracking", "About us", MessageBoxButton.OK, MessageBoxImage.Question);
        }

    }
}
