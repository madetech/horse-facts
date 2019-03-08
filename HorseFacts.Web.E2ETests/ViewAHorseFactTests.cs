using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
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
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            var webFactory = new WebApplicationFactory<Startup>();
            client = webFactory.CreateClient();
        }

        [Test]
        public async Task UserCanViewAHorseFact()
        {
            var expectedJson = JObject.FromObject(new Dictionary<string, object>
            {
                {"horseFact", "Horses use their whiskers to detect if they can fit through a space."}
            });
            
            var server = FluentMockServer.Start(new FluentMockServerSettings()
            {
                Urls = new[] { "https://cat-fact.herokuapp.com" }
            });
            
            server
                .Given(Request
                    .Create()
                    .WithPath("/facts/random")
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("{\"_id\":\"58e00be30aac31001185edfe\",\"user\":\"58e007480aac31001185ecef\",\"text\":\"Cats use their whiskers to detect if they can fit through a space.\",\"__v\":0,\"updatedAt\":\"2019-03-02T21:20:35.601Z\",\"createdAt\":\"2018-02-26T21:20:04.469Z\",\"deleted\":false,\"type\":\"cat\",\"source\":\"user\",\"used\":true}"));
            
            var result = await client.GetAsync("/api/horse-facts/random");
            var content = await result.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject(content);
            
            result.IsSuccessStatusCode.Should().BeTrue();
            expectedJson.Should().BeEquivalentTo(json);
        }
    }
}
