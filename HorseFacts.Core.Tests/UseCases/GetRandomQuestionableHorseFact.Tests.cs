using System.Threading.Tasks;
using FluentAssertions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using HorseFacts.Core.UseCases;
using Xunit;

namespace HorseFacts.Core.Tests.UseCases
{
    public class GetRandomQuestionableHorseFactTests : IAnimalFactGateway
    {
        private bool _animalFactCalled { get; set; }
        private string _animalFact { get; set; }
        private readonly GetRandomQuestionableHorseFact _subject;
        public GetRandomQuestionableHorseFactTests()
        {
            _animalFactCalled = false;
            _subject = new GetRandomQuestionableHorseFact(this);
        }

        [Fact]
        public async Task WhenCalled_ReturnsAHorseFact()
        {
            _animalFact =
                "Lil' Bunny Sue Roux is a horse who was born with no front legs, and walks upright like a kangaroo.";

            var fact = await _subject.Execute();

            AssertHorseFactToBe(
                "Lil' Bunny Sue Roux is a horse who was born with no front legs, and walks upright like a horse."
                , fact
            );
        }

        [Fact]
        public async Task WhenCalled_GetsAnAnimalFact()
        {
            _animalFact = "horses have four legs";

            await _subject.Execute();

            _animalFactCalled.Should().BeTrue();
        }

        [Fact]
        public async Task WhenCalled_GetsAnAnimalFact_AndReturnsItAsAHorseFact()
        {
            _animalFact = "horses have four legs";

            var response = await _subject.Execute();

            AssertHorseFactToBe("horses have four legs", response);
        }

        [Fact]
        public async Task WhenCalled_GetsACatBasedAnimalFact_AndReturnsItAsAHorseFact()
        {
            _animalFact = "a cat can have four legs";

            var response = await _subject.Execute();

            AssertHorseFactToBe("a horse can have four legs", response);
        }

        [Fact]
        public async Task WhenCalled_GetsACatBasedAnimalFact_WithoutManglingCatContainingWords()
        {
            _animalFact = "my cat is unable to do string concatenation";

            var response = await _subject.Execute();

            AssertHorseFactToBe("my horse is unable to do string concatenation", response);
        }

        [Fact]
        public async Task WhenCalled_GetsAPluralisedCatBasedAnimalFact_AndReturnsAPluralisedHorseFact()
        {
            _animalFact = "cats love to meow";

            var response = await _subject.Execute();

            AssertHorseFactToBe("horses love to meow", response);
        }

        [Theory]
        [InlineData("dog")]
        [InlineData("zebra")]
        [InlineData("giraffe")]
        [InlineData("lion")]
        public async Task WhenCalled_CanGetDifferentAnimalFacts_AndStillReturnThemAsHorseFacts(string animalType)
        {
            _animalFact = $"a {animalType} wags its tail when it is happy";

            var response = await _subject.Execute();

            AssertHorseFactToBe("a horse wags its tail when it is happy", response);
        }

        [Theory]
        [InlineData("dogs")]
        [InlineData("zebras")]
        [InlineData("giraffes")]
        [InlineData("lions")]
        public async Task WhenCalled_CanGetDifferentPluralisedAnimalFacts_AndStillReturnThemAsPluralisedHorseFacts(
            string pluralAnimalType)
        {
            _animalFact = $"mountain {pluralAnimalType} always land on their feet";

            var response = await _subject.Execute();

            AssertHorseFactToBe("mountain horses always land on their feet", response);
        }

        [Theory]
        [InlineData("Cats have an average of 24 whiskers.", "Horses have an average of 24 whiskers.")]
        [InlineData("I LOVE CATS", "I LOVE HORSES")]
        [InlineData("Cat cat CAT!", "Horse horse HORSE!")]
        public async Task WhenCalled_PreservesCapitalisationOfAnimalNames(string fact, string expectedFact)
        {
            _animalFact = fact;

            var response = await _subject.Execute();

            AssertHorseFactToBe(expectedFact, response);
        }

        public Task<AnimalFact> GetAnimalFact()
        {
            _animalFactCalled = true;
            return Task.FromResult(
                new AnimalFact
                {
                    Fact = _animalFact
                }
            );
        }

        private void AssertHorseFactToBe(string expectedHorseFactText, object actualResponse)
        {
            actualResponse.Should().BeEquivalentTo(new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = expectedHorseFactText
            });
        }
    }
}
