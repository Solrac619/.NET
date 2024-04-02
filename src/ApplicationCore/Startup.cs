using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationCore
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services
                .AddAutoMapper(assembly)
                .AddValidatorsFromAssembly(assembly);
        }
    }
}
