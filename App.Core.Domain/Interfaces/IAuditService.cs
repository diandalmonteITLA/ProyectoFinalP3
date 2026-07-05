using App.Core.Domain.Entities;

namespace App.Core.Application.Interfaces
{
    public interface IAuditService
    {
        Task RegisterAsync(
            string entity,
            string action,
            string user,
            string description);
    }
}