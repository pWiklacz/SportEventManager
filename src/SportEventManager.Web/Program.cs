using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using SportEventManager.Core;
using SportEventManager.Infrastructure;
using SportEventManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SportEventManager.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? userConnectionString = builder.Configuration.GetConnectionString("UserDbContextConnection") ?? throw new InvalidOperationException("Connection string 'UserDbContextConnection' not found.");
string? appConnectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found."); ;

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(userConnectionString!));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(appConnectionString!).EnableSensitiveDataLogging(true));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
  var roles = new[] { "Admin", "EventManager", "TeamManager" };
  foreach (var role in roles)
  {
    if (!await roleManager.RoleExistsAsync(role))
    {
      await roleManager.CreateAsync(new IdentityRole(role));
    }
  }
}

//using (var scope = app.Services.CreateScope())
//{
//  //var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
//  //context.Database.Migrate();
//  //context.Database.EnsureCreated();
//
//  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//
//  var emails = new[] { "admin@admin.com", "event@event.com", "team@team.com"};
//  var names = new[] {"Admin","EventManager","TeamManager"};
//  var passwords = new[] {"Admin1!", "Event1!", "Team1!"};
//
//  for (int i = 0; i < 3; i++)
//  {
//    if (await userManager.FindByEmailAsync(emails[i]) == null)
//    {
//      var user = new User()
//      {
//        FirstName = names[i],
//        LastName = names[i],
//        UserName = names[i],
//        Email = emails[i],
//        EmailConfirmed = true
//      };
//      await userManager.CreateAsync(user, passwords[i]);
//      await userManager.AddToRoleAsync(user, names[i]);
//    }
//  }
//}
//
//// Seed Database
////using (var scope = app.Services.CreateScope())
//{
//  var services = scope.ServiceProvider;
//
//  try
//  {
//    var context = services.GetRequiredService<AppDbContext>();
//    //                    context.Database.Migrate();
//    context.Database.EnsureCreated();
//    SeedData.Initialize(services);
//  }
//  catch (Exception ex)
//  {
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
//  }
//}

app.Run();
