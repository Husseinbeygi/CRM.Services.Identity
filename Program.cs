using CRM.Services.Identity.Data;
using CRM.Services.Identity.IdentityManagers;
using CRM.Services.Identity.Infrastructure;
using CRM.Services.Identity.Infrastructure.Middlewares;
using CRM.Services.Identity.Infrastructure.Settings;
using CRM.Services.Identity.MessageSenders;
using CRM.Services.Identity.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.Configure<ApplicationSettings>
	(builder.Configuration.GetSection(key: ApplicationSettings.KeyName))
	.AddSingleton
	(implementationFactory: serviceType =>
	{
		var result =
			serviceType.GetRequiredService
			<Microsoft.Extensions.Options.IOptions
			<ApplicationSettings>>().Value;

		return result;
	});

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
    AppUserStore>();

services.AddScoped<UserManager<User>, AppUserManager>();
services.AddScoped<RoleManager<Role>, AppRoleManager>();
services.AddScoped<SignInManager<User>, AppSignInManager>();
services.AddScoped<RoleStore<Role, DatabaseContext, Guid, UserRole, RoleClaim>, AppRoleStore>();
services.AddScoped<IEmailSender, EmailMessageSender>();
//services.AddScoped<ISmsSender, SmsMessageSender>();


services.AddDatabaseDeveloperPageExceptionFilter();

services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddUserStore<AppUserStore>()
              .AddUserManager<AppUserManager>()
              .AddRoleStore<AppRoleStore>()
              .AddRoleManager<AppRoleManager>()
              .AddSignInManager<AppSignInManager>()
              .AddDefaultTokenProviders();

var x509 = new X509Certificate2(
	 File.ReadAllBytes("key.pfx"),"123456789");


services.AddIdentityServer(options =>
{
    options.UserInteraction.ErrorUrl = "/Error";
    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
    options.UserInteraction.LogoutIdParameter = "logoutId";
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseErrorEvents = true;

})
    .AddInMemoryIdentityResources(InMemroyConfigurations.IdentityResources)
    .AddInMemoryClients(InMemroyConfigurations.Clients)
    .AddInMemoryApiScopes(InMemroyConfigurations.ApiScopes)
    .AddAspNetIdentity<User>()
    .AddSigningCredential(x509)
	.AddValidationKey(x509)
	.AddProfileService<ProfileManager>();



services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

services.AddRazorPages();

var app = builder.Build();

app.UseDeveloperExceptionPage();

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

app.UseCors("MyPolicy");

app.UseIdentityServer();

app.UseAuthorization();

app.UseCultureCookie();

app.MapRazorPages();

app.Run();
