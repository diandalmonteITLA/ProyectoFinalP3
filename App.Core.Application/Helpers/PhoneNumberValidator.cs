using System.Text.RegularExpressions;
using App.Core.Application.Interfaces;

namespace App.Core.Application.Helpers
{
    /*
    Valida el formato de un numero telefonico. StudentService, GuardianService y TeacherService 
    deben llamar PhoneNumberValidator en lugar de implementar la logica nuevamente.
    Solo acepta numeros de Republica Dominicana: (eg. 809, 829 or 849 seguido de 7 digitos). 
    Los formatos aceptados incluyen: 8091234567, 809-123-4567, (809) 123-4567, 809.123.4567 and +1 809-123-4567.
    */
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
