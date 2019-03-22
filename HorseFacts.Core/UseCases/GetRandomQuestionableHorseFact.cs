using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace HorseFacts.Core.UseCases
{
    public class GetRandomQuestionableHorseFact
    {
        private readonly IAnimalFactGateway _animalFactGateway;

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway)
        {
            _animalFactGateway = animalFactGateway;
        }
        
        public Dictionary<string, string> Execute()
        {
            var animals = new[] {"cat", "dog", "zebra", "giraffe", "lion"};

            var fact = _animalFactGateway.GetAnimalFact();

            var horseFact = animals.Aggregate(fact, (current, animal) =>
                Regex.Replace(current, @"\b" + animal + @"(s?)\b", "horse$1"));

            return new Dictionary<string, string>
            {
                {"horseFact", horseFact}
            };
        }
    }
}
