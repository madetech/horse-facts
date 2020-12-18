using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using HorseFacts.Core.Extensions;
using HorseFacts.Core.GatewayInterfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HorseFacts.Core.UseCases
{
    public class GetRandomVeganIpsumParagraph : IUseCase<GetRandomVeganIpsumParagraphResponse>
    {
        private readonly IFoodIpsumGateway _foodIpsumGateway;
        private readonly Regex _lowerCase;
        private readonly Regex _capitalCase;

        public GetRandomVeganIpsumParagraph(IFoodIpsumGateway foodIpsumGateway, IProvideWords meatWords)
        {
            _foodIpsumGateway = foodIpsumGateway;
            var meats = meatWords.GetWords();

            _lowerCase = new Regex($"\\b({string.Join('|', meats)})(s?)\\b", RegexOptions.Compiled);
            _capitalCase = new Regex($"\\b({string.Join('|', meats.Capitalise())})(s?)\\b", RegexOptions.Compiled);
        }

        public async Task<GetRandomVeganIpsumParagraphResponse> Execute()
        {
            var foodIpsum = await _foodIpsumGateway.GetFoodIpsum();

            var parapraph = foodIpsum.Paragraph;
            parapraph = _lowerCase.Replace(parapraph, "banana$2");
            parapraph = _capitalCase.Replace(parapraph, "Banana$2");

            return new GetRandomVeganIpsumParagraphResponse
            {
                VeganIpsumParagraph = parapraph
            };
        }
    }
}
