using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Seeds;
using Infrastructure.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProgressoRepository, ProgressoRepository>();
        services.AddScoped<IModuloRepository, ModuloRepository>();
        services.AddScoped<IConquistaRepository, ConquistaRepository>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Modulos.AnyAsync())
        {
            context.Modulos.AddRange(SeedData.ModulosSeed);
            context.Conquistas.AddRange(SeedData.ConquistasSeed);
            await context.SaveChangesAsync();
        }
    }
}