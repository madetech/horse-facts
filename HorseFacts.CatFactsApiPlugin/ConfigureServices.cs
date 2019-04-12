using System;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.GatewayInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HorseFacts.CatFactsApiPlugin
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCatFactsApiPluginServices(this IServiceCollection services)
        {
            // TODO: Don't hardcode the production API URL as a default value. Demand the environment variable be set, or blow up.
            services.AddScoped<IAnimalFactGateway, CatFactsApiGateway>(_ =>
                new CatFactsApiGateway(Environment.GetEnvironmentVariable("CAT_FACTS_API_HOSTNAME") ?? "https://cat-fact.herokuapp.com"));

            return services;
        }
    }
}
