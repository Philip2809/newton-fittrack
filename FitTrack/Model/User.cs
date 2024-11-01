using FitTrack.Utils;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class User : Person
    {
        // So that a workout manager can easily access the user's workouts
        public Guid Id { get; set; }

        // Does the user have two-factor authentication enabled?
        public bool TwoFA { get; set; } = false;

        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public override void SignIn()
        {
            if (TwoFA)
            {
                // Generate random numer and ask user for it
                Random generator = new Random();
                String r = generator.Next(0, 1000000).ToString("D6");
                // Simulate the user getting the 2FA code, run on separate thread to not stop code
                var thread = new Thread(() => {
                    MessageBox.Show(r);
                });
                thread.Start();
                var answer = Microsoft.VisualBasic.Interaction.InputBox("Enter your 2FA code", "2FA", "");

                // If answer passes these checks then let user in
                if (answer != r) {
                    Helpers.Error("Wrong code entered");
                    return;
                };
            }
            new WorkoutsWindow(this).Show();
        }

        public override bool ResetPasword()
        {
            var answer = Microsoft.VisualBasic.Interaction.InputBox("To verify it is you please answer your security question: \n" + SecurityQuestion, "Security question", "");
            if (answer?.ToLower() == SecurityAnswer.ToLower())
            {
                new UserDetailsWindow(this).Show();
                return true;
            }
            return false;
        }
    }
}
