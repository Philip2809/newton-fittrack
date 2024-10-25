using FitTrack.MVVM;
using FitTrack.View;
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

        public RelayCommand<PasswordBox> SignInCommand => new RelayCommand<PasswordBox>(pbxPassword => SignIn(pbxPassword), canExecute => usernameInput != "");
        public RelayCommand<Window> RegisterCommand => new RelayCommand<Window>(mainWindow => Register(mainWindow));

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

        public void SignIn(PasswordBox? pbxPassword)
        {
            // temporarly show a window with inputed username and password
            MessageBox.Show("Username: " + UsernameInput + "\nPassword: " + pbxPassword?.Password);
        }

        public void Register(Window? mainWindow)
        {
            new RegisterWindow().Show();
            mainWindow?.Close();
        }

    }
}
