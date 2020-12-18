using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.Domain;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace Tests
{
    public class CatFactsApiGatewayTests : IDisposable
    {
        private FluentMockServer _server;

        public CatFactsApiGatewayTests()
        {
            _server = FluentMockServer.Start(new FluentMockServerSettings());
        }

        [Theory]
        [InlineData("Cats eat hay")]
        [InlineData("Cats make a quacking noise when threatened")]
        public async Task GetAnimalFact_ReturnsAnimalFact(string factText)
        {            
            StubCatFactsRandomEndpoint(factText);

            var httpClient = new HttpClient() { BaseAddress = new Uri(_server.Urls.First()) };
            var catFactApi = new CatFactsApiGateway(httpClient);
            var animalFact = await catFactApi.GetAnimalFact();
            
            animalFact.Should().BeEquivalentTo(new AnimalFact
            {
                Fact = factText
            });
        }

        private void StubCatFactsRandomEndpoint(string responseFactText)
        {
            _server
                .Given(Request
                    .Create()
                    .WithPath("/facts/random")
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody($"{{\"_id\":\"58e00be30aac31001185edfe\",\"user\":\"58e007480aac31001185ecef\",\"text\":\"{responseFactText}\",\"__v\":0,\"updatedAt\":\"2019-03-02T21:20:35.601Z\",\"createdAt\":\"2018-02-26T21:20:04.469Z\",\"deleted\":false,\"type\":\"cat\",\"source\":\"user\",\"used\":true}}"));
        }

        public void Dispose()
        {
            _server.Stop();
        }
    }
}
