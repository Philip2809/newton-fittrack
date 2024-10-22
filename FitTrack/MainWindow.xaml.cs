using FitTrack.ViewModel;
using System.Windows;
namespace FitTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
