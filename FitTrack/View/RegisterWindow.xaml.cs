using FitTrack.MVVM;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTrack.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            // Add PasswordBoxConverter resource before InitializeComponent so it can be used in XAML
            this.Resources.Add("PasswordBoxConverter", new MultiBinderConverter<PasswordBox>());

            InitializeComponent();
            DataContext = new RegisterWindowViewModel();
        }
    }

}
