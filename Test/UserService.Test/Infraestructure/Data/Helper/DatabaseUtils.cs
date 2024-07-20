using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Infraestructure.Data.Persistence;

namespace UserService.Test.Infraestructure.Data.Helper
{
    public static class DatabaseUtils
    {
        public static DatabaseContext CreateDbContextInstance(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var dbContext = new DatabaseContext(options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public static void ClearDatabase(DatabaseContext context)
        {
            context.RemoveRange(context.Users);
            context.RemoveRange(context.Settings);
            context.SaveChanges();
        }
    }
}
