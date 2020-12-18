using System;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.GatewayInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HorseFacts.CatFactsApiPlugin
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCatFactsApiPluginServices(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Don't hardcode the production API URL as a default value. Demand the environment variable be set, or blow up.
            services.AddScoped<IAnimalFactGateway, CatFactsApiGateway>();
            services.AddHttpClient<IAnimalFactGateway, CatFactsApiGateway>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("CatFacts").GetValue<string>("Hostname"));
            });

            return services;
        }
    }
}
