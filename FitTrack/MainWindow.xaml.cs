using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.Utils;
using FitTrack.ViewModel;
using System.Windows;
namespace FitTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Add MultiBinderConverter resource before InitializeComponent so it can be used in XAML
            this.Resources.Add("MultiBinderConverter", new MultiBinderConverter());

            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
