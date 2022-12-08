using CRM.Services.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM.Services.Identity.Models;

public class AppUserStore : UserStore<User, Role, DatabaseContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
{
    public AppUserStore(DatabaseContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
