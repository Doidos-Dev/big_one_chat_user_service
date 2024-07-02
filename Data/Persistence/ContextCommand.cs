using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class ContextCommand(DbContextOptions<ContextCommand> options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SettingsModel> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextCommand).Assembly);
        }
    }
}
