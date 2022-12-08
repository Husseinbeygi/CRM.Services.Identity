using CRM.Services.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Services.Identity.Models;

public class AppRoleStore : RoleStore<Role, DatabaseContext, Guid, UserRole, RoleClaim>
{
    public AppRoleStore(DatabaseContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
