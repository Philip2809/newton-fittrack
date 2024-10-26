using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
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
    class RegisterWindowViewModel : ViewModelBase
    {
        // FitTrack is only available in the Nordic countries
        public List<string> Countries { get; } = new()
        {
            "Denmark",
            "Finland",
            "Norway",
            "Sweden",
            "Iceland",
            "Greenland",
            "Åland",
            "Faroe Islands"
        };

        private string PasswordRegex = @"^(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&¤\/\(\)=+£\\\}\]\[\{])[a-z\d@$!%*#?&¤\/\(\)=+£\\\}\]\[\{]{8,}$";

        public RelayCommand<Window> LoginCommand => new RelayCommand<Window>(registerWindow => GoBackToLogin(registerWindow));
        public RelayCommand<object[]> RegisterCommand => new RelayCommand<object[]> (parameters => Register(parameters), canExecute => countryInput != "" && usernameInput != "");

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

        private void GoBackToLogin(Window registerWindow)
        {
            new MainWindow().Show();
            registerWindow.Close();
        }

        private void Register(object[] parameters)
        {
            var password = (parameters[0] as PasswordBox)?.Password;
            var passwordConfirm = (parameters[1] as PasswordBox)?.Password;
            var registerWindow = parameters[2] as Window;
            if (password == "" || passwordConfirm == "" || password == null || passwordConfirm == null)
            {
                Helpers.Error("You shall enter your password!");
                return;
            }

            if (password != passwordConfirm)
            {
                Helpers.Error("The passwords you have entered do not match!");
                return;
            }

            Match m = Regex.Match(password, PasswordRegex, RegexOptions.IgnoreCase);
            if (!m.Success) {
                Helpers.Error("The password must include 8 characters, one number and a special character");
                return;
            }

            User user = new()
            {
                Username = usernameInput,
                Country = countryInput,
                Password = password,
                SecurityQuestion = "",
                SecurityAnswer = ""
            };

            var success = UserManager.AddUser(user);
            if (success)
            {
                new MainWindow().Show();
                registerWindow.Close();
            } else
            {
                Helpers.Error("User already exists!");
            }
        }
    }
}
