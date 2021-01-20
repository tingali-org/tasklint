using Microsoft.AspNetCore.Identity;

using System.Linq;
using System.Threading.Tasks;

using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Infrastructure.Identity;

namespace Template.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.NoteLists.Any())
            {
                context.NoteLists.Add(new NoteList {
                    Title = "Note List 1",
                    Items =
                    {
                        new NoteItem { Title = "Note 1", BackgroundColor = BackgroundColor.Blue },
                        new NoteItem { Title = "Note 2", BackgroundColor = BackgroundColor.None, Done = true },
                        new NoteItem { Title = "Note 3", BackgroundColor = BackgroundColor.Pink, Done = true },
                        new NoteItem { Title = "Note 4", BackgroundColor = BackgroundColor.Red },
                        new NoteItem { Title = "Note 5", BackgroundColor = BackgroundColor.Blue },
                        new NoteItem { Title = "Note 6", BackgroundColor = BackgroundColor.Yellow },
                        new NoteItem { Title = "Note 7", BackgroundColor = BackgroundColor.Blue, Done = true },
                        new NoteItem { Title = "Note 8", BackgroundColor = BackgroundColor.Green }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
