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
        private readonly Regex _lowerCase;
        private readonly Regex _capitalCase;
        private readonly Regex _upperCase;

        public GetRandomQuestionableHorseFact(IAnimalFactGateway animalFactGateway, IProvideWords animalWords)
        {
            _animalFactGateway = animalFactGateway;
            var animals = animalWords.GetWords();

            _lowerCase = new Regex($"\\b({string.Join('|', animals)})(s?)\\b", RegexOptions.Compiled);
            _capitalCase = new Regex($"\\b({string.Join('|', animals.Capitalise())})(s?)\\b", RegexOptions.Compiled);
            _upperCase = new Regex($"\\b({string.Join('|', animals.ToUpper())})(S?)\\b", RegexOptions.Compiled);
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
