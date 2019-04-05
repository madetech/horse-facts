using System;
using System.Net;
using System.Net.Http;
using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HorseFacts.CatFactsApiPlugin.Gateways
{
    public class CatFactsApiGateway : IAnimalFactGateway
    {
        private string _hostname;

        public CatFactsApiGateway(string hostname)
        {
            _hostname = hostname;
        }
        
        public AnimalFact GetAnimalFact()
        {
            using (var http = new WebClient())
            {
                var body = http.DownloadString($"{_hostname}/facts/random");
                var thing = JsonConvert.DeserializeObject<RandomFactResponse>(body);

                return new AnimalFact
                {
                    Fact = thing.Text
                };
            };
        }

        private class RandomFactResponse
        {
            public string Text { get; set; }
        }
    }
}
