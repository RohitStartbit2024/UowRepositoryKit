using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UowRepositoryKit.Interfaces;
using UowRepositoryKit.Uow;

namespace UowRepositoryKit.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUowRepositoryKit<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
