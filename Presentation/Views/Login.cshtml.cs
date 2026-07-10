using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Application.DTOs.User;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        [Required(ErrorMessage = "El correo electrónico o usuario es obligatorio.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                Response.Redirect(ReturnUrl ?? "/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _accountService.AuthenticateAsync(new LoginDto
            {
                UserName = Email,
                Password = Password
            });

            if (result != null && !result.HasError)
            {
                return LocalRedirect(ReturnUrl ?? "/Index");
            }

            ErrorMessage = result?.Errors?.FirstOrDefault() ?? "Usuario o contraseña incorrectos.";
            return Page();
        }
    }
}
