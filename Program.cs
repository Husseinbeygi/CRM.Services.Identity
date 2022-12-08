using CRM.Services.Identity.Data;
using CRM.Services.Identity.IdentityManagers;
using CRM.Services.Identity.Infrastructure;
using CRM.Services.Identity.Infrastructure.Middlewares;
using CRM.Services.Identity.Infrastructure.Settings;
using CRM.Services.Identity.MessageSenders;
using CRM.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


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
    .AddDeveloperSigningCredential()
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
