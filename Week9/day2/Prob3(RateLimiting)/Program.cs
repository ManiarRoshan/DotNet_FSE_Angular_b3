
using Con_Mgmt_Cach_Pag_RateLimiting.Models;
using Con_Mgmt_Cach_Pag_RateLimiting.Repositories;
using Con_Mgmt_Cach_Pag_RateLimiting.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Con_Mgmt_Cach_Pag_RateLimiting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 1. Register Memory Cache
            builder.Services.AddMemoryCache();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // 2. Register Layers for Dependency Injection
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IContactService, ContactService>();
            //For pagination
            builder.Services.AddScoped<IContactRepository, ContactRepository>();

            //For Rate Limit
            builder.Services.AddRateLimiter(options =>
            {
                // 1. Configure the 429 Response message
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = async (context, token) =>
                {
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
                };

                // 2. Define the "FixedWindow" Policy
                options.AddFixedWindowLimiter(policyName: "fixed-policy", opt =>
                {
                    opt.PermitLimit = 5;                          // Max 5 requests
                    opt.Window = TimeSpan.FromSeconds(60);        // Per 60 seconds
                    opt.QueueLimit = 0;                           // Don't queue extra requests, reject them
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            // rate limit middleware
            app.UseRateLimiter();
            app.MapControllers();

            app.Run();
        }
    }
}
