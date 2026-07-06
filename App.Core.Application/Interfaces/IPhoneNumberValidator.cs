namespace App.Core.Application.Interfaces
{
    /*
    Define un contrato para validar el formato de un numero telefonico. 
    Utilizado por clases como GuardianService y TeacherService. 
    Su proposito es prevenir codigo duplicado
    */
    public interface IPhoneNumberValidator
    {
        bool ValidateNumber(string number);
    }
}
