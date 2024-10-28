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
    class ResetPasswordWindowViewModel : ViewModelBase
    {
        public RelayCommand<Window> ResetPasswordCommand => new RelayCommand<Window> (window => ResetPassword(window));

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

        private void ResetPassword(Window window)
        {
            var user = UserManager.users.Find(u => u.Username == usernameInput);
            if (user == null)
            {
                Helpers.Error("User not found");
                return;
            }

            var success = user.ResetPasword();
            if (success)
            {
                window?.Close();
            }
            else
            {
                Helpers.Error("Password reset failed");
            }
        }
    }
}
