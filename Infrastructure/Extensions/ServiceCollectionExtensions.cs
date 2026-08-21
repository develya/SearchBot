using Application;
using Infrastructure.DomRia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<DomRiaOptions>(
            configuration.GetSection("DomRia"));
        services.AddHttpClient<DomRiaClient>(client =>
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        });
        services.AddScoped<IPropertySource, DomRiaPropertySource>();

        return services;
    }
}
