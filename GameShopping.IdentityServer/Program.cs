using GameShopping.IdentityServer.Configuration;
using GameShopping.IdentityServer.Configuration.Initializer;
using GameShopping.IdentityServer.Model;
using GameShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];

builder.Services.AddDbContext<MySqlContext>(options => options.
UseMySql(connection,
new MySqlServerVersion(new Version(5, 7, 4))));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySqlContext>().AddDefaultTokenProviders();

var identityBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;


}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
.AddInMemoryClients(IdentityConfiguration.Clients)
.AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();


identityBuilder.AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
var dbInitializer = app.Services.GetService<IDbInitializer>();
dbInitializer.Initialize();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
