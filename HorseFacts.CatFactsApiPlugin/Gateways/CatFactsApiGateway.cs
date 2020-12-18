using System.Net.Http;
using System.Threading.Tasks;
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
        
        public async Task<AnimalFact> GetAnimalFact()
        {
            var body = await _httpClient.GetStringAsync("/facts/random");
            var text = JToken.Parse(body).Value<string>("text");
            return new AnimalFact
            {
                Fact = text
            };
        }
    }
}
