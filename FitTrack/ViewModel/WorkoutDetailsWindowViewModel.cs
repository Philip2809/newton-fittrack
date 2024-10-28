using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.ViewModel
{
    class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        public RelayCommand<Window> CloseSaveCommand => new RelayCommand<Window>(window => CloseSave(window));
        public RelayCommand<object> EditCancelCommand => new RelayCommand<object>(parameter => EditCancel());
        public RelayCommand<object> CopyCommand => new RelayCommand<object>(parameter => Copy(), canExecute => isViewOnly);

        public User User { get; set; }
        public Workout Workout { get; set; }
        public WorkoutDetailsWindowViewModel(User user, Workout workout)
        {
            User = user;
            Workout = workout;

            fillFields();
        }

        // Fill fields with data from the workout, also use this if the user decided to cancel the edit to revert the changes
        private void fillFields()
        {
            TypeInput = Workout.Type;
            DateInput = Workout.Date;
            DurationInput = Workout.Duration.ToString();
            CaloriesInput = Workout.CaloriesBurned.ToString();
            NotesInput = Workout.Notes;
        }

        private bool isViewOnly = true;
        public bool IsViewOnly
        {
            get { return isViewOnly; }
            set
            {
                isViewOnly = value;
                OnPropertyChanged();
            }
        }

        private string typeInput = "";
        public string TypeInput
        {
            get { return typeInput; }
            set
            {
                typeInput = value;
                OnPropertyChanged();
            }
        }

        private DateTime dateInput = DateTime.Now;
        public DateTime DateInput
        {
            get { return dateInput; }
            set
            {
                dateInput = value;
                OnPropertyChanged();
            }
        }

        private string durationInput = "";
        public string DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
                OnPropertyChanged();
            }
        }

        private string caloriesInput = "";
        public string CaloriesInput
        {
            get { return caloriesInput; }
            set
            {
                caloriesInput = value;
                OnPropertyChanged();
            }
        }

        private string notesInput = "";
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged();
            }
        }


        private void CloseSave(Window window)
        {
             // If not in view mode update the workout
             if (!isViewOnly)
             {
                TimeSpan duration;
                if (!TimeSpan.TryParse(durationInput, out duration))
                {
                    Helpers.Error("Duration needs to be valid, try something like 'hh:mm:ss'.");
                    return;
                }

                int calories;
                if (!int.TryParse(caloriesInput, out calories))
                {
                    Helpers.Error("Calories burned needs to be a number.");
                    return;
                }

                if (typeInput == "" || caloriesInput == "" || notesInput == "")
                {
                    Helpers.Error("All inputfields needs to be filled in to be able to save");
                    return;
                }

                WorkoutManager.UpdateWorkout(new() {
                    Type = typeInput,
                    Date = dateInput,
                    Duration = duration,
                    CaloriesBurned = calories,
                    Notes = notesInput,
                });

            }

            new WorkoutsWindow(User).Show();
            window?.Close();
        }

        private void EditCancel()
        {
            if (isViewOnly)
            {
                IsViewOnly = false;
                return;
            }
            IsViewOnly = true;
            fillFields(); // Revert changes
        }

        private void Copy()
        {
            new AddWorkoutWindow(User, Workout).Show();
        }
    }
}
