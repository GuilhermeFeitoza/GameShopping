using AutoMapper;
using GameShopping.ProductAPI.Config;
using GameShopping.ProductAPI.Model.Context;
using GameShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {

    options.Authority = "https://localhost:4435/";
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        ValidateAudience = false,
    }; 


});

builder
    .Services.AddAuthorization(options =>
    {

        options.AddPolicy("ApiScope", policy => {

            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope","game_shopping");

        
        
        }
        );

    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GameShopping.ProductAPI",Version = "v1"});
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme { 
        Description = @"Enter 'Bearer'  [Space ] and  your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme= "Bearer"
    
    
    });;

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { 
   
        {
         new OpenApiSecurityScheme
         {
                Reference = new OpenApiReference
                {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"


                },
                Scheme ="oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header

         },
         new List<string>() 
    }
    });

});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];


builder.Services.AddDbContext<MySqlContext>(options => options.
UseMySql(connection,
new MySqlServerVersion(new Version(5, 7, 4))));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
