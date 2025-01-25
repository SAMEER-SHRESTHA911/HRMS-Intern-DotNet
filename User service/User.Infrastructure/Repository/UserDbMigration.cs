using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Infrastructure.Repository
{
    public class UserDbMigration
    {
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = new UserDbContext(serviceScope.ServiceProvider.GetService<DbContextOptions<UserDbContext>>()))
                {
                    var databaseName = context.Database.GetDbConnection().Database;
                    if (databaseName == "userDb")
                    {
                        context.Database.Migrate();
                        DbInitializer.Initialize(context);
                    }
                }
            }
        }
    }
}
