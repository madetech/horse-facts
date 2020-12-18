using System.Linq;

namespace HorseFacts.Core.Extensions
{
    public static class StringExtensions
    {
        public static string[] Capitalise(this string[] strings)
        {
            return strings.Select(s => s[0..1].ToUpper() + s[1..]).ToArray();
        }
    }
}
