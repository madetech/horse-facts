using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.GatewayInterfaces;

namespace HorseFacts.Core.UseCases
{
    public class GetRandomQuestionableHorseFact : IGetRandomQuestionableHorseFact
    {
        private readonly IAnimalFactGateway _animalFactGateway;

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway)
        {
            _animalFactGateway = animalFactGateway;
        }

        public GetRandomQuestionableHorseFactResponse Execute()
        {
            var animals = new[]
            {
                "cat", "dog", "zebra", "giraffe", "lion", "tabby", "tabbie", "jaguar",
                "squirrel", "lemur", "elephant", "gorilla", "kitten", "bird", "snake",
                "monkey", "ape", "koala", "kangaroo", "penguin", "bear", "tiger", "goose",
                "duck", "swan", "snail", "slug", "ant", "wasp", "bee", "hornet", "insect",
                "spider", "scorpion", "millipede", "centipede", "owl", "hedgehog", "wolf",
                "dragon", "rhino", "fox", "narwhal", "unicorn", "fish", "shark", "dolphin",
                "octopus", "whale", "sloth", "cheetah", "ocelot", "tuna", "cod", "haddock",
                "mackerel", "kipper"
            };

            var fact = _animalFactGateway.GetAnimalFact();

            return new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = ReplaceAnimalsWithHorsesInFact(fact.Fact, animals)
            };
        }

        private string ReplaceAnimalsWithHorsesInFact(string fact, string[] animals)
        {
            return animals.Aggregate(fact, (current, animal) =>
                {
                    current = Regex.Replace(current, @"\b" + animal + @"(s?)\b", "horse$1");
                    current = Regex.Replace(current, @"\b" + animal.Substring(0, 1).ToUpper() + animal.Substring(1) + @"(s?)\b", "Horse$1");
                    current = Regex.Replace(current, @"\b" + animal.ToUpper() + @"(S?)\b", "HORSE$1");

                    return current;
                });
        }
    }
}
