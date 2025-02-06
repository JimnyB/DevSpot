using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync(Roles.Admin).Result)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!roleManager.RoleExistsAsync(Roles.JobSeeker).Result)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }

            if (!roleManager.RoleExistsAsync(Roles.Empolyer).Result)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Empolyer));
            }
        }
    }
}
