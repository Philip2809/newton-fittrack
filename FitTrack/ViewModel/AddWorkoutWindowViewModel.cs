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
    class AddWorkoutWindowViewModel : ViewModelBase
    {
        public RelayCommand<Window> SaveCommand => new RelayCommand<Window>(window => Save(window));

        public User User { get; set; }
        public AddWorkoutWindowViewModel(User user, Workout? template)
        {
            User = user;
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


        private void Save(Window window)
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

            WorkoutManager.AddWorkout(User, new()
            {
                Type = typeInput,
                Date = dateInput,
                Duration = duration,
                CaloriesBurned = calories,
                Notes = notesInput,
            });

            window.Close();
        }
    }
}
