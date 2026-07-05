using System.Text.RegularExpressions;
using App.Core.Application.Interfaces;

namespace App.Core.Application.Helpers
{
    /// <summary>
    /// Validates the format of a phone number. This is the single place that owns
    /// this rule; StudentService, GuardianService and TeacherService should call
    /// ValidateNumber instead of re-implementing the check.
    /// Only accepts Dominican Republic numbers: area code 809, 829 or 849, followed
    /// by 7 digits. Accepted formats include: 8091234567, 809-123-4567,
    /// (809) 123-4567, 809.123.4567 and +1 809-123-4567.
    /// </summary>
    public class PhoneNumberValidator : IPhoneNumberValidator
    {
        private static readonly Regex PhoneNumberPattern = new(
            @"^(\+1[-.\s]?)?\(?(809|829|849)\)?[-.\s]?\d{3}[-.\s]?\d{4}$",
            RegexOptions.Compiled);

        public bool ValidateNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            return PhoneNumberPattern.IsMatch(number.Trim());
        }
    }
}
