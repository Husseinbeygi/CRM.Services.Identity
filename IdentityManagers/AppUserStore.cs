using CRM.Services.Identity.Data;
using CRM.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Services.Identity.IdentityManagers;

public class AppUserStore : UserStore<User, Role, DatabaseContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
{
    public AppUserStore(DatabaseContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
