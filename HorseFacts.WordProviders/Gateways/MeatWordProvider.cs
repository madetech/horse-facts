using HorseFacts.Core.GatewayInterfaces;
using System.Linq;

namespace HorseFacts.WordProviders.Gateways
{
    public class MeatWordProvider : IProvideWords
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

        public string[] GetWords()
        {
            return _meats
                .GroupBy(meat => meat.Split(' ').Length, meat => meat)
                .OrderByDescending(g => g.Key)
                .SelectMany(g => g)
                .ToArray();
        }
    }
}
