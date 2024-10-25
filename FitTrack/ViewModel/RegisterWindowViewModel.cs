using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        public RelayCommand<Window> LoginCommand => new RelayCommand<Window>(registerWindow => GoBackToLogin(registerWindow));
        public RelayCommand<PasswordBox[]> RegisterCommand => new RelayCommand<PasswordBox[]> (passwordBoxes => Register(passwordBoxes), canExecute => countryInput != "" && usernameInput != "");

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

        private void Register(PasswordBox[] passwordBoxes)
        {
            var pbxInput = passwordBoxes[0];
            var pbxInputConfirm = passwordBoxes[1];
            // temporarly show a window with inputed username and password
            MessageBox.Show("Username: " + UsernameInput + "\nCountry: " + CountryInput);
            MessageBox.Show(pbxInput?.Password + " " +  pbxInputConfirm?.Password);
        }
    }
}
