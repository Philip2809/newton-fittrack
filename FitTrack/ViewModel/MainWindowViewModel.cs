using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public RelayCommand SignInCommand => new RelayCommand(parameter => SignIn(parameter), canExecute => usernameInput != "");

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

        public void SignIn(object parameter)
        {
            var pbxPassword = parameter as PasswordBox;
            // temporarly show a window with inputed username and password
            MessageBox.Show("Username: " + UsernameInput + "\nPassword: " + pbxPassword?.Password);
        }

        public void Register()
        {
            
        }

    }
}
