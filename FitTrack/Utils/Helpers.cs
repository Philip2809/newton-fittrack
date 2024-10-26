using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Utils
{
    class Helpers
    {
        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string PasswordRegex = @"^(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&¤\/\(\)=+£\\\}\]\[\{])[a-z\d@$!%*#?&¤\/\(\)=+£\\\}\]\[\{]{8,}$";

        // FitTrack is only available in the Nordic countries
        public static List<string> Countries { get; } = new()
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

        public static bool IsPasswordGood(string? password, string? passwordConfirm)
        {
            if (password == "" || passwordConfirm == "" || password == null || passwordConfirm == null)
            {
                Error("You shall enter your password!");
                return false;
            }

            if (password != passwordConfirm)
            {
                Error("The passwords you have entered do not match!");
                return false;
            }

            // Check password via regex
            Match m = Regex.Match(password, PasswordRegex, RegexOptions.IgnoreCase);
            if (!m.Success)
            {
                Error("The password must include 8 characters, one number and a special character");
                return false;
            }

            return true;
        }
    }
}
