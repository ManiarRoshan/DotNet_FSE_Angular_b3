using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

namespace AuthApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           //Authentication Scheme 'Bearer'
            // The key MUST be the same as your Auth Service
            var secretKey = Encoding.UTF8.GetBytes("ThisIsMyVerySecureSecretKey1234567890");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options => // This name "Bearer" must match ocelot.json
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = true,
                        ValidIssuer = "AuthService",
                        ValidateAudience = true,
                        ValidAudience = "ApiGateway",
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // 2. Load Ocelot configuration
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // 3. Register Ocelot
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            // 4. Middlewares (Order is vital)
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
