using Microsoft.AspNetCore.Identity;

namespace FTN_Skriptarnica_ASPNET.Data
{
    public class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roles = services.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var name in new[] { "User", "Admin" })
                if (!await roles.RoleExistsAsync(name))
                    await roles.CreateAsync(new IdentityRole(name));
        }
    }
}
