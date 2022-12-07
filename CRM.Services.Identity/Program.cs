using CRM.Services.Identity.Data;
using CRM.Services.Identity.Infrastructure;
using CRM.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));


services.AddScoped<
    UserStore<User,Role, DatabaseContext,
    Guid,
    UserClaim,
    UserRole,
    UserLogin,
    UserToken,
    RoleClaim>,
    CRM.Services.Identity.Models.AppUserStore>();

services.AddScoped<UserManager<User>, AppUserManager>();
services.AddScoped<RoleManager<Role>, AppRoleManager>();
services.AddScoped<SignInManager<User>, AppSignInManager>();
services.AddScoped<RoleStore<Role, DatabaseContext, Guid, UserRole, RoleClaim>, AppRoleStore>();


services.AddDatabaseDeveloperPageExceptionFilter();

services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddUserStore<AppUserStore>()
              .AddUserManager<AppUserManager>()
              .AddRoleStore<AppRoleStore>()
              .AddRoleManager<AppRoleManager>()
              .AddSignInManager<AppSignInManager>()
              .AddDefaultTokenProviders();

services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemroyConfigurations.IdentityResources)
    .AddInMemoryClients(InMemroyConfigurations.Clients)
    .AddInMemoryApiScopes(InMemroyConfigurations.ApiScopes)
    .AddAspNetIdentity<User>()
    .AddDeveloperSigningCredential();




services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
