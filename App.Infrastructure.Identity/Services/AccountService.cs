using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Application.DTOs.Email;
using App.Core.Application.DTOs.User;
using App.Core.Application.Interfaces;
using App.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using Org.BouncyCastle.Security;

namespace App.Infrastructure.Identity.Services
{
    public class AccountServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginDto loginDto)
        {
            LoginResponseDto response = new() { Email = "", Id = "", LastName = "", Name = "", UserName = "", HasError = false, Errors = [] };

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"No hay usuario registrado con este email: {loginDto.UserName}");
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Errors.Add("Su cuenta aun no esta activada, deberias revisar tu email");
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName ?? "", loginDto.Password, false, true);

            if (!result.Succeeded)
            {
                response.HasError = true;
                if (result.IsLockedOut)
                {
                    response.Errors.Add($"Su cuenta ha sido bloqueada debido a multiples intentos de acceso fallidos." +
                        $" Intente nuevamente en 10 minutos.");
                }
                else
                {
                    response.Errors.Add($"Email o contraseña incorrecta");
                }
                return response;
            }

            var rolesList = await _userManager.GetRolesAsync(user);

            response.Id = user.Id;
            response.Email = user.Email ?? "";
            response.UserName = user.UserName ?? "";
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.IsVerified = user.EmailConfirmed;
            response.Roles = rolesList.ToList();

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUser, string origin)
        {
            RegisterResponseDto response = new() { HasError = false, Errors = [] };

            var userWithSameEmail = await _userManager.FindByNameAsync(registerUser.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Errors.Add("Este email ya esta registrado");
                return response;
            }

            AppUser user = new AppUser()
            {
                Name = registerUser.Name,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                UserName = registerUser.Email,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerUser.Role);
                string verificationUri = await GetVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new EmailRequestDto()
                {
                    To = registerUser.Email,
                    HtmlBody = $"Activa tu cuenta mediante este URL: {verificationUri}",
                    Subject = "Activa tu cuenta",
                });
            }
            else
            {
                response.HasError = true;
                response.Errors.Add("Un error ocurrió al intentar registrar al usuario.");
                return response;
            }
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No hay una cuenta registrada con este email";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta {user.Email} confirmada. Ahora puedes acceder al sistema";
            }
            else
            {
                return $"Un error ha ocurrido confirmando este email {user.Email}";
            }
        }


        public async Task<EditResponseDto> EditUser(RegisterUserDto registerUser, string origin, bool? isCreated = false)
        {
            bool isNotcreated = !isCreated ?? false;
            EditResponseDto response = new()
            {
                Email = "",
                Id = "",
                LastName = "",
                Name = "",
                UserName = "",
                HasError = false,
                Errors = []
            };

            var userWithSameUserName = await _userManager.Users.FirstOrDefaultAsync(w => w.UserName == registerUser.Email && w.Id != registerUser.Id);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Errors.Add($"El email {registerUser.Email} ya esta en el sistema");
                return response;
            }

            var user = await _userManager.FindByIdAsync(registerUser.Id);

            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is no acccount registered with this user");
                return response;
            }

            user.Name = registerUser.Name;
            user.LastName = registerUser.LastName;
            user.UserName = registerUser.Email;
            user.EmailConfirmed = user.EmailConfirmed && user.Email == registerUser.Email;
            user.Email = registerUser.Email;
            user.PhoneNumber = registerUser.Phone;

            if (!string.IsNullOrWhiteSpace(registerUser.Password) && isNotcreated)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultChange = await _userManager.ResetPasswordAsync(user, token, registerUser.Password);

                if (resultChange != null && !resultChange.Succeeded)
                {
                    response.HasError = true;
                    response.Errors.AddRange(resultChange.Errors.Select(s => s.Description).ToList());
                    return response;
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var rolesList = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, rolesList.ToList());

                await _userManager.AddToRoleAsync(user, registerUser.Role);


                if (!user.EmailConfirmed && isNotcreated)
                {
                    string verificationUri = await GetVerificationEmailUri(user, origin);
                    await _emailService.SendAsync(new EmailRequestDto()
                    {
                        To = registerUser.Email,
                        HtmlBody = $"Please confirm yout account visiting this URL {verificationUri}",
                        Subject = "Confirm registration"
                    });
                }

                var updatedRolesList = await _userManager.GetRolesAsync(user);

                response.Id = user.Id;
                response.Email = user.Email ?? "";
                response.UserName = user.UserName ?? "";
                response.Name = user.Name;
                response.LastName = user.LastName;
                response.IsVerified = user.EmailConfirmed;
                response.Roles = updatedRolesList.ToList();

                return response;
            }
            else
            {
                response.HasError = true;
                response.Errors.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;
            }
        }


        #region Private methods
        private async Task<string> GetVerificationEmailUri(AppUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ConfirmEmail";
            var completeUrl = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri.ToString(), "token", token);
            return "";
        }
        #endregion
    }
}
