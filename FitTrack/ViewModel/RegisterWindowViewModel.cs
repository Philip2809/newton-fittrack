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
        public List<string> Countries { get { return Helpers.Countries; } } 
        public RelayCommand<Window> LoginCommand => new RelayCommand<Window>(registerWindow => GoBackToLogin(registerWindow));
        public RelayCommand<object[]> RegisterCommand => new RelayCommand<object[]> (parameters => Register(parameters), canExecute => countryInput != "" && usernameInput != "" && securityQuestionInput != "");

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

        private string securityQuestionInput = "";
        public string SecurityQuestionInput
        {
            get { return securityQuestionInput; }
            set
            {
                securityQuestionInput = value;
                OnPropertyChanged();
            }
        }

        private string securityAnswerInput = "";
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged();
            }
        }

        private void GoBackToLogin(Window window)
        {
            new MainWindow().Show();
            window.Close();
        }

        private void Register(object[] parameters)
        {
            var password = (parameters[0] as PasswordBox)?.Password;
            var passwordConfirm = (parameters[1] as PasswordBox)?.Password;
            var window = parameters[2] as Window;

            if (!Helpers.IsPasswordGood(password, passwordConfirm))
            {
                return;
            }

            User user = new()
            {
                Username = usernameInput,
                Country = countryInput,
                Password = password,
                SecurityQuestion = securityQuestionInput,
                SecurityAnswer = securityAnswerInput
            };

            var success = UserManager.AddUser(user);
            if (success)
            {
                new MainWindow().Show();
                window?.Close();
            } else
            {
                Helpers.Error("User already exists!");
            }
        }
    }
}
