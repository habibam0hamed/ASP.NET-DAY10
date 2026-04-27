using Microsoft.AspNetCore.Identity;

public static class RoleSeeder
{
    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Moderator"))
            await roleManager.CreateAsync(new IdentityRole("Moderator"));
    }
}