using CRM.Services.Identity.Data.Configurations;
using CRM.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;

namespace CRM.Services.Identity.Data
{
    public class DatabaseContext : 
        IdentityDbContext<User,Role,Guid,
            UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            };


            //builder.ApplyConfigurationsFromAssembly(typeof(UserConfigurations).Assembly);
        }
    }
}