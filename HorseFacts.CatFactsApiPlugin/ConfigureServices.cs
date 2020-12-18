using System;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.GatewayInterfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCatFactsApiPluginServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAnimalFactGateway, CatFactsApiGateway>();
            services.AddHttpClient<IAnimalFactGateway, CatFactsApiGateway>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("CatFacts").GetValue<string>("Hostname"));
            });

            services.AddScoped<IFoodIpsumGateway, BaconIpsumApiGateway>();
            services.AddHttpClient<IFoodIpsumGateway, BaconIpsumApiGateway>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("BaconIpsum").GetValue<string>("Hostname"));
            });

            return services;
        }
    }
}
