using HorseFacts.Core.UseCases;
using HorseFacts.WordProviders.Gateways;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureWordProviders(this IServiceCollection services)
        {
            services.AddWordProvider<GetRandomQuestionableHorseFact, AnimalWordProvider>();
            services.AddWordProvider<GetRandomVeganIpsumParagraph, MeatWordProvider>();

            return services;
        }

        public static void AddWordProvider<TImplementation, TWordProvider>(this IServiceCollection services) where TWordProvider : new()
        {
            var oldDescriptor = services.First(sd => sd.ImplementationType == typeof(TImplementation));
            Func<IServiceProvider, object> factory = sp => ActivatorUtilities.CreateInstance(sp, oldDescriptor.ImplementationType, new TWordProvider());
            var newDescriptor = new ServiceDescriptor(oldDescriptor.ServiceType, factory, ServiceLifetime.Scoped);
            var index = services.IndexOf(oldDescriptor);
            services.RemoveAt(index);
            services.Insert(index, newDescriptor);
        }
    }
}
