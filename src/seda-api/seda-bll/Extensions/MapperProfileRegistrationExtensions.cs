using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using seda_bll.MapperProfiles;

namespace seda_bll.Extensions;

public static class MapperProfileRegistrationExtensions
{
    public static void RegisterMapperProfiles(this IServiceCollection services)
    {
        var config = new MapperConfiguration(c =>
        {
            c.AddProfile<UserProfile>();
            c.AddProfile<IncidentDocumentProfile>();
        });

        services.AddSingleton(config.CreateMapper());
    }
}