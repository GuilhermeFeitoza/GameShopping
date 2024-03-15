using GameShopping.IdentityServer.Model;
using GameShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GameShopping.IdentityServer.Configuration.Initializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly MySqlContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySqlContext context,
        UserManager<ApplicationUser> user,
        RoleManager<IdentityRole> role)
        {

        _context = context;
        _user = user;
        _role = role;

        }
        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "guilherme-admin",
                Email = "guilherme@admin.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11_ 12345-1234",
                FirstName = "Guilherme",
                LastName = "Admin",
            };
            _user.CreateAsync(admin,"Gui123-").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name , $"{admin.FirstName} {admin.LastName}" ),
                new Claim(JwtClaimTypes.FamilyName , $"{admin.LastName}"),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Admin)
            }).Result;


            ApplicationUser client = new ApplicationUser()
            {
                UserName = "guilherme-client",
                Email = "guilherme@client.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11_ 12345-1234",
                FirstName = "Guilherme",
                LastName = "client",
            };
            _user.CreateAsync(client, "Gui123-").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name , $"{client.FirstName} {client.LastName}" ),
                new Claim(JwtClaimTypes.FamilyName , $"{client.LastName}"),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Client)
            }).Result;
        }
    }
}
