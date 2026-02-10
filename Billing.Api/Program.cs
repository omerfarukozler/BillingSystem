using Billing.Infra.Data;
using Billing.Infra.Auth;
using Billing.Infra.Data.Repositories;
using Billing.Domain.Account;
using Billing.Domain.Customer;
using Billing.App;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Billing.Infra.Security;
using Billing.App.Ports;
using Billing.Api.Adapters;
using Billing.Api.Middleware;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Routing (lowercase urls + lowercase [controller])
// ----------------------------
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    // options.LowercaseQueryStrings = true; // istersen aç
});

// ----------------------------
// Database
// ----------------------------
builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseSqlite("Data Source=billing.db"));

// ----------------------------
// Controllers
// ----------------------------
builder.Services.AddControllers(options =>
{
    // [controller], [action] tokenlarını lowercase'a çevirir (Swagger path dahil)
    options.Conventions.Add(new RouteTokenTransformerConvention(new LowercaseTransformer()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------
// Dependency Injection
// ----------------------------
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentAccount, HttpCurrentAccount>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<CustomerService>();

// ----------------------------
// JWT Authentication
// ----------------------------
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwt["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateLifetime = true
        };
    });

// ----------------------------
// Build App
// ----------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection(); // MapControllers'dan önce

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ----------------------------
// Helpers
// ----------------------------
sealed class LowercaseTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
        => value?.ToString()?.ToLowerInvariant();
}
