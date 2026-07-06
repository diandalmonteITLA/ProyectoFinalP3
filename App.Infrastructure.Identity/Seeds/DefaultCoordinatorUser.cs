using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Infrastructure.Identity.Seeds
{
    public static class DefaultCoordinatorUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            AppUser user = new()
            {
                Name = "John",
                LastName = "Doe",
                Email = "usuario@email.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserName = "usuario@email.com"
            };

            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var entityUser = await userManager.FindByEmailAsync(user.Email);
                if (entityUser == null)
                {
                    await userManager.CreateAsync(user, "12Pa$$word!");
                    await userManager.AddToRoleAsync(user, Roles.Coordinator.ToString());
                }
            }
        }
    }
}
