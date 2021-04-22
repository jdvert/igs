using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace IgsMarket.Api.Setup
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabaseOnStartup(this IApplicationBuilder app, bool dropBeforeMigration = false)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<IgsMarketDbContext>();

            try
            {
                if (dropBeforeMigration)
                {
                    dbContext.Database.EnsureDeleted();
                }
                
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger(typeof(ApplicationBuilderExtensions).Namespace);

                logger.LogError(ex, $"Failed to migrate database on startup - {ex.Message}");
                throw;
            }            
        }
    }
}
