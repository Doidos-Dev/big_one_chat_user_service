using Microsoft.EntityFrameworkCore;
using UserService.Test.Domain.Entities;


namespace UserService.Test.Infraestructure.Data.Persistence
{
    public class ContextCommand : DbContext
    {
        public ContextCommand(DbContextOptions<ContextCommand> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<SettingsModel> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextCommand).Assembly);
        }
    }
}
