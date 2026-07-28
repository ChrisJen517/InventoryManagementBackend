using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InventoryApi.Models;
using InventoryApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "https://localhost:7213",
                                              "https://inventory-management-three-dun-22.vercel.app")
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials();
                      });
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!builder.Environment.IsDevelopment())
{
    var connectionStringsSection = builder.Configuration.GetSection("ConnectionStrings");
    foreach (var connString in connectionStringsSection.GetChildren())
    {
        // connectionString.Key is the name, connectionString.Value is the actual connection string
        Console.WriteLine($"Key: {connString.Key}");
        Console.WriteLine($"Value: {connString.Value}");
        Console.WriteLine("-----------------------------------");
    }
    // connectionString = builder.Configuration.GetConnectionString("MyDbConnection");
    // if (string.IsNullOrEmpty(connectionString))
    // {
    //     connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_MyDbConnection"); ;
    //     if (string.IsNullOrEmpty(connectionString))
    //     {
    //         throw new InvalidOperationException("Connection string 'AZURE_MYSQL_CONNECTIONSTRING' not found.");
    //     }
    // }
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<UserIdentity>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<UserIdentity>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false; // Prevent JavaScript access
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Force HTTPS

    // Override default MVC redirect behavior for API contexts (Return 401 instead of redirecting to a login page)
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.MapIdentityApi<UserIdentity>();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
