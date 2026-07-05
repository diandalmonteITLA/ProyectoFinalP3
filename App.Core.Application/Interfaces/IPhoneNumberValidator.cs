namespace App.Core.Application.Interfaces
{
    /// <summary>
    /// Defines a single-responsibility contract for validating phone number formats.
    /// Used by any entity that owns a phone number (Student, Guardian, Teacher),
    /// so the format rule lives in one place instead of being duplicated.
    /// </summary>
    public interface IPhoneNumberValidator
    {
        bool ValidateNumber(string number);
    }
}
