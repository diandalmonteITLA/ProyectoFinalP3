using App.Core.Application.DTOs.User;
using App.Infrastructure.Identity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using App.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using App.Core.Application.ViewModels.Login;

namespace App.Presentation.Web.Controllers
{
    public class LoginController : Controller
    {
            private readonly IAccountService _accountService;

 
            public LoginController(IAccountService accountServiceForWebApp)
            {
                _accountService = accountServiceForWebApp;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    return await RedirectBasedOnRole(User.Identity.Name ?? "");
                }

                return View(new LoginViewModel() { UserName = "", Password = ""});
            }

            [HttpPost]
            public async Task<IActionResult> Index(LoginViewModel vm)
            {
                if (!ModelState.IsValid)
                {
                    vm.Password = "";
                    return View(vm);
                }

                var userDto = await _accountService.AuthenticateAsync(new LoginDto
                {
                    Password = vm.Password,
                    UserName = vm.UserName
                });

                if (userDto != null && !userDto.HasError)
                {
                    if (userDto.Roles != null && userDto.Roles.Contains(Roles.Admin.ToString()))
                    {
                        throw new NotImplementedException();
                    }

                    return RedirectToAction("Index", "Student");
                }

                foreach (var error in userDto?.Errors ?? [])
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                vm.Password = "";
                return View(vm);
            }

            [HttpPost]
            public async Task<IActionResult> Logout()
            {
                await _accountService.SignOutAsync();
                return RedirectToAction("Index", "Login");
            }

            [HttpGet]
            public async Task<IActionResult> AccessDenied()
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    var user = await _accountService.GetUserByUserName(User.Identity.Name ?? "");
                    ViewBag.CurrentRol = user?.Role ?? "";
                    return View();
                }

                return RedirectToAction("Index", "Login");
            }

            #region Private Helpers

            private async Task<IActionResult> RedirectBasedOnRole(string userName)
            {
                var user = await _accountService.GetUserByUserName(userName);

                if (user != null)
                {
                    if (user.Role == Roles.Admin.ToString())
                    {
                        throw new NotImplementedException();
                    }
                    if (user.Role == Roles.Coordinator.ToString())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                return View(new LoginViewModel() { UserName="", Password="" });
            }
            #endregion
    }
}
