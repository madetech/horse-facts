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
        private static readonly string[] _meats = new string[]
        {
            "beef", "chicken", "pork", "bacon", "chuck", "short loin", "sirloin",
            "shank", "flank", "sausage", "pork belly", "shoulder", "cow", "pig",
            "ground round", "hamburger", "meatball", "tenderloin", "strip steak",
            "t-bone", "ribeye", "shankle", "tongue", "tail", "pork chop", "pastrami",
            "corned beef", "jerky", "ham", "fatback", "ham hock", "pancetta", "pork loin",
            "short ribs", "spare ribs", "beef ribs", "drumstick", "tri-tip", "ball tip",
            "venison", "turkey","biltong", "rump", "jowl", "salami", "bresaola", "meatloaf",
            "brisket", "boudin", "andouille", "capicola", "swine", "kielbasa", "frankfurter",
            "prosciutto", "filet mignon", "leberkas", "turducken", "doner", "kevin",
            "landjaeger", "porchetta", "alcatra", "picanha", "cupim", "burgdoggen", "buffalo"
        };

        private readonly IFoodIpsumGateway _foodIpsumGateway;
        private readonly Regex _lowerCase = new Regex($"\\b({string.Join('|', _meats)})(s?)\\b", RegexOptions.Compiled);
        private readonly Regex _capitalCase = new Regex($"\\b({string.Join('|', _meats.Capitalise())})(s?)\\b", RegexOptions.Compiled);

        public GetRandomVeganIpsumParagraph(IFoodIpsumGateway foodIpsumGateway)
        {
            _foodIpsumGateway = foodIpsumGateway;
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
