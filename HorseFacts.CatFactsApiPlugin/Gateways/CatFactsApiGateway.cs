using System.Net.Http;
using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using Newtonsoft.Json.Linq;

namespace HorseFacts.CatFactsApiPlugin.Gateways
{
    public class CatFactsApiGateway : IAnimalFactGateway
    {
        private readonly HttpClient _httpClient;

        public CatFactsApiGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public AnimalFact GetAnimalFact()
        {
            var body = _httpClient.GetStringAsync("/facts/random").Result;
            var text = JToken.Parse(body).Value<string>("text");
            return new AnimalFact
            {
                Fact = text
            };
        }
    }
}
