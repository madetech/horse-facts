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
        private readonly IProvideWords _meatWords;

        public GetRandomVeganIpsumParagraph(IFoodIpsumGateway foodIpsumGateway, IProvideWords meatWords)
        {
            _foodIpsumGateway = foodIpsumGateway;
            _meatWords = meatWords;
        }

        public async Task<GetRandomVeganIpsumParagraphResponse> Execute()
        {
            var foodIpsum = await _foodIpsumGateway.GetFoodIpsum();
            var meats = _meatWords.GetWords();

            var parapraph = foodIpsum.Paragraph;
            parapraph = Regex.Replace(parapraph, $"\\b({string.Join('|', meats)})(s?)\\b", "banana$2");
            parapraph = Regex.Replace(parapraph, $"\\b({string.Join('|', meats.Capitalise())})(s?)\\b", "Banana$2");

            return new GetRandomVeganIpsumParagraphResponse
            {
                VeganIpsumParagraph = parapraph
            };
        }
    }
}
