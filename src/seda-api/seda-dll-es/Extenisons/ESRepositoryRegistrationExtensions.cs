using Microsoft.Extensions.DependencyInjection;
using seda_dll_es.Contracts;
using seda_dll_es.Models;

namespace seda_dll_es.Extensions;

public static class ESRepositoryRegistrationExtensions
{
    public static void RegisterESRepositories(this IServiceCollection services)
    {
        services.AddScoped<IElasticRepository<ESIncidentDocument>, IncidentDocumentElasticRepository>();
    }
}