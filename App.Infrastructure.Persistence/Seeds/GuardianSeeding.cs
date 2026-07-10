using App.Core.Domain.Entities;
using App.Core.Domain.Enums;
using App.Infrastructure.Persistence.Contexts;

namespace App.Infrastructure.Persistence.Seeds
{
    public class GuardianSeeding
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var guardians = new List<Guardian>
            {
                new Guardian { Name = "Luis", LastName="Mendoza", Email="luismndza@email.com",
                    PhoneNumbers = [ new PhoneNumber(){
                    Number = "8098339871", Type= NumberType.Mobile}, new PhoneNumber(){Number="8498060955", Type = NumberType.Home} ]},
                new Guardian { Name = "Mario", LastName="Hernandez", Email="mariohernandez@email.com",
                    PhoneNumbers = [ new PhoneNumber(){
                    Number = "8298339188", Type= NumberType.Mobile},]},
                new Guardian { Name = "Maria", LastName="Terrero", Email="marterrero@email.com",
                    PhoneNumbers = [ new PhoneNumber(){
                    Number = "8098039927", Type= NumberType.Mobile} ]}
            };

            foreach (Guardian guardian in guardians)
            {
                await context.Set<Guardian>().AddAsync(guardian);
            }

            await context.SaveChangesAsync();
        }
    }
}
