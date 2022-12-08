using CRM.Services.Identity.Data;
using CRM.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Services.Identity.IdentityManagers;

public class AppRoleStore : RoleStore<Role, DatabaseContext, Guid, UserRole, RoleClaim>
{
    public AppRoleStore(DatabaseContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
