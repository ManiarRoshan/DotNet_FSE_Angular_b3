using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load Ocelot configuration
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Register Ocelot
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            // Ocelot Middleware
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
