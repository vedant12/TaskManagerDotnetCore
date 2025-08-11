

using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems { get; set; }
}