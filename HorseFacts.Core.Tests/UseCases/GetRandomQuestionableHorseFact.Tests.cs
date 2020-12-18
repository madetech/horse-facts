using System.Collections.Generic;
using FluentAssertions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Core.Domain;
using HorseFacts.Core.GatewayInterfaces;
using HorseFacts.Core.UseCases;
using NUnit.Framework;

namespace HorseFacts.Core.Tests.UseCases
{
    public class GetRandomQuestionableHorseFactTests : IAnimalFactGateway
    {
        private bool _animalFactCalled { get; set; }
        private string _animalFact { get; set; }
        private GetRandomQuestionableHorseFact subject;

        [SetUp]
        public void Setup()
        {
            _animalFactCalled = false;
            subject = new GetRandomQuestionableHorseFact(this);
        }

        [Test]
        public void WhenCalled_ReturnsAHorseFact()
        {
            _animalFact =
                "Lil' Bunny Sue Roux is a horse who was born with no front legs, and walks upright like a kangaroo.";

            var fact = subject.Execute();

            AssertHorseFactToBe(
                "Lil' Bunny Sue Roux is a horse who was born with no front legs, and walks upright like a horse."
                , fact
            );
        }

        [Test]
        public void WhenCalled_GetsAnAnimalFact()
        {
            subject.Execute();

            _animalFactCalled.Should().BeTrue();
        }

        [Test]
        public void WhenCalled_GetsAnAnimalFact_AndReturnsItAsAHorseFact()
        {
            _animalFact = "horses have four legs";

            var response = subject.Execute();

            AssertHorseFactToBe("horses have four legs", response);
        }

        [Test]
        public void WhenCalled_GetsACatBasedAnimalFact_AndReturnsItAsAHorseFact()
        {
            _animalFact = "a cat can have four legs";

            var response = subject.Execute();

            AssertHorseFactToBe("a horse can have four legs", response);
        }

        [Test]
        public void WhenCalled_GetsACatBasedAnimalFact_WithoutManglingCatContainingWords()
        {
            _animalFact = "my cat is unable to do string concatenation";

            var response = subject.Execute();

            AssertHorseFactToBe("my horse is unable to do string concatenation", response);
        }

        [Test]
        public void WhenCalled_GetsAPluralisedCatBasedAnimalFact_AndReturnsAPluralisedHorseFact()
        {
            _animalFact = "cats love to meow";

            var response = subject.Execute();

            AssertHorseFactToBe("horses love to meow", response);
        }

        [Test]
        [TestCase("dog")]
        [TestCase("zebra")]
        [TestCase("giraffe")]
        [TestCase("lion")]
        public void WhenCalled_CanGetDifferentAnimalFacts_AndStillReturnThemAsHorseFacts(string animalType)
        {
            _animalFact = $"a {animalType} wags its tail when it is happy";

            var response = subject.Execute();

            AssertHorseFactToBe("a horse wags its tail when it is happy", response);
        }

        [Test]
        [TestCase("dogs")]
        [TestCase("zebras")]
        [TestCase("giraffes")]
        [TestCase("lions")]
        public void WhenCalled_CanGetDifferentPluralisedAnimalFacts_AndStillReturnThemAsPluralisedHorseFacts(
            string pluralAnimalType)
        {
            _animalFact = $"mountain {pluralAnimalType} always land on their feet";

            var response = subject.Execute();

            AssertHorseFactToBe("mountain horses always land on their feet", response);
        }

        [Test]
        [TestCase("Cats have an average of 24 whiskers.", "Horses have an average of 24 whiskers.")]
        [TestCase("I LOVE CATS", "I LOVE HORSES")]
        [TestCase("Cat cat CAT!", "Horse horse HORSE!")]
        public void WhenCalled_PreservesCapitalisationOfAnimalNames(string fact, string expectedFact)
        {
            _animalFact = fact;

            var response = subject.Execute();

            AssertHorseFactToBe(expectedFact, response);
        }

        public AnimalFact GetAnimalFact()
        {
            _animalFactCalled = true;
            return new AnimalFact
            {
                Fact = _animalFact
            };
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
