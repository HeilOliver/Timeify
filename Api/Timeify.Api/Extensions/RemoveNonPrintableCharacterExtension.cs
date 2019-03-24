using System.Text.RegularExpressions;

namespace Timeify.Api.Extensions
{
    public static class RemoveNonPrintableCharacterExtension
    {
        public static string RemoveNonPrintableCharacter(this string self)
        {
            return Regex.Replace(self, @"\p{C}+", string.Empty);
        }
    }
}