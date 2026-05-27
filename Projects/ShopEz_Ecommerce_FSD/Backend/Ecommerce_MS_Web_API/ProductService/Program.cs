using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.FileProviders;
using ProductService.Data;
using ProductService.Repositories;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IProductService, ProductServices>();

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Print JWT settings at startup for verification
Console.WriteLine($"ProductService JWT Settings:");
Console.WriteLine($"  Issuer: {jwtIssuer}");
Console.WriteLine($"  Audience: {jwtAudience}");
Console.WriteLine($"  Key (first 10 chars): {jwtKey?.Substring(0, Math.Min(10, jwtKey?.Length ?? 0))}...");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
        // logging events
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"ProductService: JWT Authentication Failed - {context.Exception.Message}");
                if (context.Exception is SecurityTokenInvalidIssuerException)
                    Console.WriteLine("   -> Invalid issuer. Check that the token's issuer matches the configured issuer.");
                else if (context.Exception is SecurityTokenInvalidAudienceException)
                    Console.WriteLine("   -> Invalid audience. Check that the token's audience matches the configured audience.");
                else if (context.Exception is SecurityTokenInvalidSignatureException)
                    Console.WriteLine("   -> Invalid signature. Check that the JWT key matches.");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"ProductService: JWT Challenge - {context.Error}, {context.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("ProductService: JWT Token Validated Successfully");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Ensure wwwroot/images exists
var imagesPath = Path.Combine(app.Environment.WebRootPath, "images");
if (!Directory.Exists(imagesPath))
    Directory.CreateDirectory(imagesPath);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "images")),
    RequestPath = "/images"
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
