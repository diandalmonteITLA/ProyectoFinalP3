using App.Core.Application.Interfaces;

namespace App.Core.Application.Services
{
    public class AuditService : IAuditService
    {
        public Task RegisterAsync(
            string entity,
            string action,
            string description)
        {
            Console.WriteLine(
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {entity} - {action} - {description}");

            return Task.CompletedTask;
        }
    }
}