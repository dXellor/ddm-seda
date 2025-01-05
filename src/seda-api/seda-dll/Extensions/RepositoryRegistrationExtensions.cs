using Microsoft.Extensions.DependencyInjection;
using seda_dll.Contracts;
using seda_dll.Repositories;

namespace seda_dll.Extensions;

public static class RepositoryRegistrationExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IIncidentDocumentRepository, IncidentDocumentRepository>();
    }
}