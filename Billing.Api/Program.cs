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

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Database
// ----------------------------
builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseSqlite("Data Source=billing.db"));

// ----------------------------
// Controllers
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------
// Dependency Injection 
// ----------------------------

// Domain interfaces → Infra implementations
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

// JWT generator (Infra)
// Domain interface → Infra implementation
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


// App katmanı servisleri
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<CustomerService>();

// ----------------------------
// JWT Authentication
// ----------------------------
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
