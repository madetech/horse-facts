using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HorseFacts.CatFactsApiPlugin.Gateways
{
    public class BaconIpsumApiGateway : IFoodIpsumGateway
    {
        private readonly HttpClient _httpClient;

        public BaconIpsumApiGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FoodIpsum> GetFoodIpsum()
        {
            var baconIpsum = await _httpClient.GetStringAsync("/api/?type=meat-and-filler&sentences=3&start-with-lorem=0");
            var paragraph = JArray.Parse(baconIpsum).FirstOrDefault()?.Value<string>();

            return new FoodIpsum
            {
                Paragraph = paragraph
            };
        }
    }
}
