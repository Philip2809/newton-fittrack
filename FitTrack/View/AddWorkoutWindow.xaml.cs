using FitTrack.Model;
using FitTrack.ViewModel;
using System.Windows;

namespace FitTrack.View
{
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow(User user, Workout? template)
        {
            InitializeComponent();
            DataContext = new AddWorkoutWindowViewModel(user, template);
        }
    }
}
