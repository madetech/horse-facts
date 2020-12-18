using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.UseCases;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<GetRandomQuestionableHorseFactResponse>, GetRandomQuestionableHorseFact>();
            services.AddScoped<IUseCase<GetRandomVeganIpsumParagraphResponse>, GetRandomVeganIpsumParagraph>();

            return services;
        }
    }
}
