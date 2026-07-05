namespace App.Core.Application.Interfaces
{
    public interface IAuditService
    {
        Task RegisterAsync(
            string entity,
            string action,
            string description);
    }
}