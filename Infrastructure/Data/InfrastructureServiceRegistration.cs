using Application.Contracts.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IBusRepository, BusRepository>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IBusService, BusService>(); 
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IBusRepository, BusRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();



        return services;
    }
}
