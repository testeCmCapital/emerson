using System.Linq;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            System.IServiceProvider serviceProvider = host.Services;
            IServiceScopeFactory scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            UpdateDatabase(scopeFactory).Wait();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task UpdateDatabase(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                IdentityContext identityContext = scope.ServiceProvider.GetService<IdentityContext>();
                if (identityContext.Database.GetPendingMigrations().Any()) await identityContext.Database.MigrateAsync();
            }
        }
    }
}
