using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.Extensions;
using HorseFacts.Core.GatewayInterfaces;

namespace HorseFacts.Core.UseCases
{
    public class GetRandomQuestionableHorseFact : IUseCase<GetRandomQuestionableHorseFactResponse>
    {
        private static string[] _animals = new[]
            {
                "cat", "dog", "zebra", "giraffe", "lion", "tabby", "tabbie", "jaguar",
                "squirrel", "lemur", "elephant", "gorilla", "kitten", "bird", "snake",
                "monkey", "ape", "koala", "kangaroo", "penguin", "bear", "tiger", "goose",
                "duck", "swan", "snail", "slug", "ant", "wasp", "bee", "hornet", "insect",
                "spider", "scorpion", "millipede", "centipede", "owl", "hedgehog", "wolf",
                "dragon", "rhino", "fox", "narwhal", "unicorn", "fish", "shark", "dolphin",
                "octopus", "whale", "sloth", "cheetah", "ocelot", "tuna", "cod", "haddock",
                "mackerel", "kipper", "leopard", "kitty", "kittie", "housecat", "escalator"
            };

        private readonly IAnimalFactGateway _animalFactGateway;
        private readonly Regex _lowerCase = new Regex($"\\b({string.Join('|', _animals)})(s?)\\b", RegexOptions.Compiled);
        private readonly Regex _capitalCase = new Regex($"\\b({string.Join('|', _animals.Capitalise())})(s?)\\b", RegexOptions.Compiled);
        private readonly Regex _upperCase = new Regex($"\\b({string.Join('|', _animals.ToUpper())})(S?)\\b", RegexOptions.Compiled);

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway)
        {
            _animalFactGateway = animalFactGateway;
        }

        public async Task<GetRandomQuestionableHorseFactResponse> Execute()
        {
            var fact = await _animalFactGateway.GetAnimalFact();

            return new GetRandomQuestionableHorseFactResponse
            {
                HorseFact = ReplaceAnimalsWithHorsesInFact(fact.Fact)
            };
        }

        private string ReplaceAnimalsWithHorsesInFact(string fact)
        {
            var horseFact = fact;
            horseFact = _lowerCase.Replace(horseFact, "horse$2");
            horseFact = _capitalCase.Replace(horseFact, "Horse$2");
            horseFact = _upperCase.Replace(horseFact, "HORSE$2");
            return horseFact;
        }
    }
}
