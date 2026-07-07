using App.Core.Application.DTOs.Email;
using App.Core.Application.Interfaces;
using System.Threading.Tasks;

namespace App.Infrastructure.Identity.Services
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(EmailRequestDto emailRequestDto)
        {
            return Task.CompletedTask;
        }
    }
}
