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
    class UserDetailsWindowViewModel : ViewModelBase
    {
        public List<string> Countries { get { return Helpers.Countries; } }
        public RelayCommand<Window> CancelCommand => new RelayCommand<Window>(window => Cancel(window));
        public RelayCommand<object[]> SaveCommand => new RelayCommand<object[]>(parameters => Save(parameters));

        public User User { get; set; }
        public UserDetailsWindowViewModel(User user)
        {
            User = user;
            UsernameInput = user.Username;
            CountryInput = user.Country;
            TwoFAInput = user.TwoFA;
        }

        private string usernameInput = "";
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
            }
        }

        private bool twoFAInput = false;
        public bool TwoFAInput
        {
            get { return twoFAInput; }
            set
            {
                twoFAInput = value;
                OnPropertyChanged();
            }
        }

        private string countryInput = "";
        public string CountryInput
        {
            get { return countryInput; }
            set
            {
                countryInput = value;
                OnPropertyChanged();
            }
        }

        private void Cancel(Window window)
        {
            new WorkoutsWindow(User).Show();
            window.Close();
        }

        private void Save(object[] parameters)
        {
            var password = (parameters[0] as PasswordBox)?.Password;
            var passwordConfirm = (parameters[1] as PasswordBox)?.Password;
            var window = parameters[2] as Window;

            if (!Helpers.IsPasswordGood(password, passwordConfirm))
            {
                return;
            }

            if (usernameInput.Length < 3)
            {
                Helpers.Error("Username must be at least 3 characters long");
                return;
            }

            var updatedUser = UserManager.UpdateUser(User, usernameInput, countryInput, password, twoFAInput);
            if (updatedUser != null)
            {
                new WorkoutsWindow(updatedUser).Show();
                window?.Close();
            }
            else
            {
                Helpers.Error("User already exists!");
            }
        }
    }
}
