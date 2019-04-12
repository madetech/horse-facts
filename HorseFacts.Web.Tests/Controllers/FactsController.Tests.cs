using FluentAssertions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace HorseFacts.Web.Tests.Controllers
{
    public class FactsControllerTests
    {
        private Mock<IGetRandomQuestionableHorseFact> _getRandomQuestionableHorseFact;

        [SetUp]
        public void Setup()
        {
            _getRandomQuestionableHorseFact = new Mock<IGetRandomQuestionableHorseFact>();
        }

        [Test]
        [TestCase("Approximately 80% of orange horses are male")]
        [TestCase("The horse is one of the most endangered animals in the world")]
        public void Random_ReturnsAHorseFact(string expectedFact)
        {
            var stubResponse = new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = expectedFact
            };

            _getRandomQuestionableHorseFact
                .Setup(use => use.Execute())
                .Returns(stubResponse);

            var controller = new FactsController(_getRandomQuestionableHorseFact.Object);
            var response = controller.Random();

            response.Should().BeEquivalentTo(new OkObjectResult(stubResponse));
        }
    }
}
