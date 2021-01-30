
using System.Linq;
using System.Threading.Tasks;

using TaskLint.Domain.Entities;
using TaskLint.Domain.Enums;

namespace TaskLint.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.TaskLists.Any())
            {
                context.TaskLists.Add(new TaskList {
                    Title = "Task List 1",
                    Items =
                    {
                        new TaskItem { Title = "Task 1", BackgroundColor = BackgroundColor.Blue },
                        new TaskItem { Title = "Task 2", BackgroundColor = BackgroundColor.None, Done = true },
                        new TaskItem { Title = "Task 3", BackgroundColor = BackgroundColor.Pink, Done = true },
                        new TaskItem { Title = "Task 4", BackgroundColor = BackgroundColor.Red },
                        new TaskItem { Title = "Task 5", BackgroundColor = BackgroundColor.Blue },
                        new TaskItem { Title = "Task 6", BackgroundColor = BackgroundColor.Yellow },
                        new TaskItem { Title = "Task 7", BackgroundColor = BackgroundColor.Blue, Done = true },
                        new TaskItem { Title = "Task 8", BackgroundColor = BackgroundColor.Green }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
