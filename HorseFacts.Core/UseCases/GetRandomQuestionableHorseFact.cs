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
        private readonly IAnimalFactGateway _animalFactGateway;
        private readonly IProvideWords _animalWords;

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway, IProvideWords animalWords)
        {
            _animalFactGateway = animalFactGateway;
            _animalWords = animalWords;
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
            var animals = _animalWords.GetWords();

            var horseFact = fact;
            horseFact = Regex.Replace(horseFact, $"\\b({string.Join('|', animals)})(s?)\\b", "horse$2");
            horseFact = Regex.Replace(horseFact, $"\\b({string.Join('|', animals.Capitalise())})(s?)\\b", "Horse$2");
            horseFact = Regex.Replace(horseFact, $"\\b({string.Join('|', animals.ToUpper())})(S?)\\b", "HORSE$2");
            return horseFact;
        }
    }
}
