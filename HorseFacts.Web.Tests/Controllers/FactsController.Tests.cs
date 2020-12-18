using FluentAssertions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace HorseFacts.Web.Tests.Controllers
{
    public class FactsControllerTests
    {
        private Mock<IUseCase<GetRandomQuestionableHorseFactResponse>> _getRandomQuestionableHorseFact;

        public FactsControllerTests()
        {
            _getRandomQuestionableHorseFact = new Mock<IUseCase<GetRandomQuestionableHorseFactResponse>>();
        }

        [Theory]
        [InlineData("Approximately 80% of orange horses are male")]
        [InlineData("The horse is one of the most endangered animals in the world")]
        public async Task Random_ReturnsAHorseFact(string expectedFact)
        {
            var stubResponse = new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = expectedFact
            };

            _getRandomQuestionableHorseFact
                .Setup(use => use.Execute())
                .Returns(Task.FromResult(stubResponse));

            var controller = new FactsController(_getRandomQuestionableHorseFact.Object);
            var response = await controller.Random();

            response.Should().BeEquivalentTo(new OkObjectResult(stubResponse));
        }
    }
}
