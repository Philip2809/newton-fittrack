using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

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
        public ICollectionView WorkoutsView { get; set; }

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

        private string searchInput = "";
        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                searchInput = value;
                OnPropertyChanged();
                WorkoutsView.Refresh();

            }
        }
        public WorkoutsWindowViewModel(User user)
        {
            User = user;
            Workouts = WorkoutManager.GetWorkoutsByUser(User);
            // For automatic updates of the DataGrid
            WorkoutManager.workouts.CollectionChanged += OnWorkoutsCollectionChanged;
            // Apply the filter based on the search input
            WorkoutsView = CollectionViewSource.GetDefaultView(Workouts);
            WorkoutsView.Filter = Filter;
        }

        // Do the actual filtering
        private bool Filter(object obj)
        {
            if (obj is Workout workout)
            {
                // Check if the search input is in any of the properties of the workout, check lower to lower for case insensitivity, could have used "StringComparison.OrdinalIgnoreCase"
                return workout.Type.ToLower().Contains(SearchInput.ToLower()) ||
                    workout.Date.ToString().ToLower().Contains(SearchInput.ToLower()) ||
                    workout.Duration.ToString().ToLower().Contains(SearchInput.ToLower()) ||
                    // Yes, notes are not visible but it can be valuable for the user to be able to search by this, maybe they remember a keyword from the notes
                    workout.Notes.ToString().ToLower().Contains(SearchInput.ToLower());
            }
            return false;
        }

        private void OnWorkoutsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            // When using filters this becomes more complex, template code for how to handle the different types of changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Workout newWorkout in e.NewItems)
                {
                    Workouts.Add(newWorkout);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Workout oldWorkout in e.OldItems)
                {
                    Workouts.Remove(oldWorkout);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Workouts.Clear();
                var updatedWorkouts = WorkoutManager.GetWorkoutsByUser(User);
                foreach (var workout in updatedWorkouts)
                {
                    Workouts.Add(workout);
                }
            }

            // Refresh the view after updates
            WorkoutsView.Refresh();
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
            MessageBox.Show("FitTrack, a new startup in the fittness industry wants to help you to track your fittness journey, to do so, add a new workout here so you can look back onto it!", "About us", MessageBoxButton.OK, MessageBoxImage.Question);
        }

    }
}
