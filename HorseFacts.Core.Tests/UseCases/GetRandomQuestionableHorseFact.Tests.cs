using System.Collections.Generic;
using FluentAssertions;
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

            var expected = new Dictionary<string, string>
            {
                { "horseFact", "Lil' Bunny Sue Roux is a horse who was born with no front legs, and walks upright like a kangaroo." }
            };
                
            fact.Should().BeEquivalentTo(expected);
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
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "horses have four legs"}
            });
        }
        
        [Test]
        public void WhenCalled_GetsACatBasedAnimalFact_AndReturnsItAsAHorseFact()
        {
            _animalFact = "a cat can have four legs";
            
            var response = subject.Execute();
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "a horse can have four legs"}
            });
        }
        
        [Test]
        public void WhenCalled_GetsACatBasedAnimalFact_WithoutManglingCatContainingWords()
        {
            _animalFact = "my cat is unable to do string concatenation";
            
            var response = subject.Execute();
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "my horse is unable to do string concatenation"}
            });
        }
        
        [Test]
        public void WhenCalled_GetsAPluralisedCatBasedAnimalFact_AndReturnsAPluralisedHorseFact()
        {
            _animalFact = "cats love to meow";
            
            var response = subject.Execute();
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "horses love to meow"}
            });
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
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "a horse wags its tail when it is happy"}
            });
        }
        
        [Test]
        [TestCase("dogs")]
        [TestCase("zebras")]
        [TestCase("giraffes")]
        [TestCase("lions")]
        public void WhenCalled_CanGetDifferentPluralisedAnimalFacts_AndStillReturnThemAsPluralisedHorseFacts(string pluralAnimalType)
        {
            _animalFact = $"mountain {pluralAnimalType} always land on their feet";
            
            var response = subject.Execute();
            
            response.Should().BeEquivalentTo(new Dictionary<string, string>
            {
                {"horseFact", "mountain horses always land on their feet"}
            });
        }

        public string GetAnimalFact()
        {
            _animalFactCalled = true;
            return _animalFact;
        }
    }
}
