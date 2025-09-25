using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grocery.Core.Helpers {
    public static class EmailHelper {
        private static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsValidEmail(string email) {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Length is < 5 or > 254)
                return false;

            if (!EmailRegex.IsMatch(email))
                return false;

            string[] parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            string localPart = parts[0];
            string domainPart = parts[1];

            if (localPart.Length is < 1 or > 64)
                return false;

            if (domainPart.Length is < 1 or > 253)
                return false;

            if (email.Contains(".."))
                return false;

            if (localPart.StartsWith('.') || localPart.EndsWith('.'))
                return false;

            return true;
        }
    }
}
