using FitTrack.MVVM;
using FitTrack.Utils;
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

        public RelayCommand<object[]> SignInCommand => new RelayCommand<object[]>(parameters => SignIn(parameters), canExecute => usernameInput != "");
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

        public void SignIn(object[] parameters)
        {
            MessageBox.Show(parameters?.GetType().Name);
            var pbxPassword = parameters[0] as PasswordBox;
            var mainWindow = parameters[1] as Window;
            // temporarly show a window with inputed username and password
            var user = UserManager.AuthenticateUser(UsernameInput, pbxPassword?.Password);
            if (user != null)
            {
                user.SignIn();
                mainWindow?.Close();
            }
            else
            {
                Helpers.Error("Invalid username or password");
            }
        }

        public void Register(Window? mainWindow)
        {
            new RegisterWindow().Show();
            mainWindow?.Close();
        }

    }
}
