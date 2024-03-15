using GameShopping.Web.Services;
using GameShopping.Web.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IProductService, ProductService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]
    ));

builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication(options =>
{

    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

})
 .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
 .AddOpenIdConnect(options =>
 {
     options.Authority = builder.Configuration["ServicesUrls:IdentityServer"];
     options.GetClaimsFromUserInfoEndpoint = true;
     options.ClientId = "game_shopping";
     options.ClientSecret = "my_super_secret";
     options.ResponseType = OpenIdConnectResponseType.Code;
     options.ClaimActions.MapJsonKey("role", "role", "role");
     options.ClaimActions.MapJsonKey("sub", "sub", "sub");
     options.TokenValidationParameters.NameClaimType = "name";
     options.TokenValidationParameters.RoleClaimType = "role";
     options.Scope.Add("game_shopping");
     options.SaveTokens = true; 
 });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
