using Microsoft.Extensions.DependencyInjection;
using seda_bll.Contracts;
using seda_bll.Services;

namespace seda_bll.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIncidentDocumentService, IncidentDocumentService>();
        services.AddScoped<IFileManagementService, FileManagementService>();
    }
}