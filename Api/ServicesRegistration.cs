using Application.Interfaces;
using Application.Validators;
using FluentValidation;
using Infrastructure.DomRia.Mapping;
using Infrastructure.Persistence.Repository;
using MediatR;

namespace Api;

public class ServicesRegistration
{
    public static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISearchRequestRepository, SearchRequestRepository>();
    }
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<DomRiaPropertyEntityMapper>();
        services.AddScoped<PropertyDtoMapper>();
        
        services.AddValidatorsFromAssembly(typeof(BaseValidator<>).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Application.Users.UserDto).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }
    
   
}

//патерн Service Registration Extension