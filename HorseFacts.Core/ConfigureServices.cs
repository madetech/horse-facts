using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HorseFacts.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IGetRandomQuestionableHorseFact, GetRandomQuestionableHorseFact>();

            return services;
        }
    }
}
