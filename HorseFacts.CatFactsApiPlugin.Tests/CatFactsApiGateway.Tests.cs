using System;
using System.Linq;
using FluentAssertions;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.Domain;
using NUnit.Framework;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Tests
{
    public class CatFactsApiGatewayTests
    {
        private FluentMockServer _server;

        [SetUp]
        public void Setup()
        {
            _server = FluentMockServer.Start(new FluentMockServerSettings());
        }

        [TearDown]
        public void Teardown()
        {
            _server.Stop();
        }

        [Test]
        [TestCase("Cats eat hay")]
        [TestCase("Cats make a quacking noise when threatened")]
        public void GetAnimalFact_ReturnsAnimalFact(string factText)
        {            
            StubCatFactsRandomEndpoint(factText);
            
            var catFactApi = new CatFactsApiGateway(_server.Urls.First());
            var animalFact = catFactApi.GetAnimalFact();
            
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
    }
}
