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
using System.Windows.Input;
using System.Xml.Linq;

namespace FitTrack.ViewModel
{
    class AddWorkoutWindowViewModel : ViewModelBase
    {
        public List<string> WorkoutTypes { get { return Enum.GetNames(typeof(WorkoutType)).ToList(); } }
        public RelayCommand<Window> SaveCommand => new RelayCommand<Window>(window => Save(window));

        public User User { get; set; }
        public AddWorkoutWindowViewModel(User user, Workout? template)
        {
            User = user;

            TypeInput = template?.Type ?? "";
            DateInput = template?.Date ?? DateTime.Now;
            DurationInput = template?.Duration.ToString(@"hh\:mm\:ss") ?? "";
            CaloriesInput = template?.CaloriesBurned.ToString() ?? "";
            NotesInput = template?.Notes ?? "";
            
            // Additional input based on workout type
            if (template is StrengthWorkout) AdditionalInput = ((StrengthWorkout)template)?.Repetitions.ToString() ?? "";
            else if (template is CardioWorkout) AdditionalInput = ((CardioWorkout)template)?.Distance.ToString() ?? "";
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

        private string additionalInput = "";
        public string AdditionalInput
        {
            get { return additionalInput; }
            set
            {
                additionalInput = value;
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
            // check if all inputfields are filled in
            if (typeInput == "" || caloriesInput == "" || notesInput == "" || durationInput == "" || caloriesInput == "" || additionalInput == "")
            {
                Helpers.Error("All inputfields needs to be filled in to be able to save");
                return;
            }

            // Validate input
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

            WorkoutType type;
            if (Enum.TryParse(typeInput, out type) && type == WorkoutType.Cardio)
            {
                // If cardio, its distance
                int distance;
                if (!int.TryParse(additionalInput, out distance))
                {
                    Helpers.Error("Distance needs to be a number.");
                    return;
                }

                WorkoutManager.AddWorkout(User, new CardioWorkout()
                {
                    Date = dateInput,
                    Duration = duration,
                    Distance = distance,
                    CaloriesBurned = calories,
                    Notes = notesInput,
                });
            }
            else if (type == WorkoutType.Strength)
            {
                // If strength, its repetitions
                int repetitions;
                if (!int.TryParse(additionalInput, out repetitions))
                {
                    Helpers.Error("Repetitions needs to be a number.");
                    return;
                }

                WorkoutManager.AddWorkout(User, new StrengthWorkout()
                {
                    Date = dateInput,
                    Duration = duration,
                    Repetitions = repetitions,
                    CaloriesBurned = calories,
                    Notes = notesInput,
                });
            }

            window.Close();
        }
    }
}
