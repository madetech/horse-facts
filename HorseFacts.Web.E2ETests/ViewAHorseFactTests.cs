using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using HorseFacts.CatFactsApiPlugin.Gateways;
using HorseFacts.Core.GatewayInterfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace HorseFacts.Web.E2ETests
{
    public class ViewAHorseFactTests
    {
        private HttpClient _client;
        private FluentMockServer _server;

        [SetUp]
        public void Setup()
        {
            _server = FluentMockServer.Start(new FluentMockServerSettings());
            _client = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddHttpClient<IAnimalFactGateway, CatFactsApiGateway>(client =>
                    {
                        client.BaseAddress = new Uri(_server.Urls.First());
                    });
                });
            }).CreateClient();
        }

        // TODO: Make errors not involve debugging and inspecting HTML to get an awkward stack trace
        // TODO: Reconsider E2E vs subcutaneous acceptance testing split
        [Test]
        public async Task UserCanViewAHorseFact()
        {
            var expectedJson = JToken.Parse("{\"horseFact\": \"Horses use their whiskers to detect if they can fit through a space.\"}");

            _server
                .Given(Request
                    .Create()
                    .WithPath("/facts/random")
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("{\"_id\":\"58e00be30aac31001185edfe\",\"user\":\"58e007480aac31001185ecef\",\"text\":\"Cats use their whiskers to detect if they can fit through a space.\",\"__v\":0,\"updatedAt\":\"2019-03-02T21:20:35.601Z\",\"createdAt\":\"2018-02-26T21:20:04.469Z\",\"deleted\":false,\"type\":\"cat\",\"source\":\"user\",\"used\":true}"));

            var result = await _client.GetAsync("/api/facts/random");
            var content = await result.Content.ReadAsStringAsync();
            var json = JToken.Parse(content);

            result.StatusCode.Should().Be(200);
            JToken.DeepEquals(expectedJson, json).Should().BeTrue();
        }
    }
}
