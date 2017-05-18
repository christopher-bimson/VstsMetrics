using System.Text.RegularExpressions;

namespace VstsMetrics.Extensions
{
    public static class StringExtensions
    {
        public static string PascalCaseToTitleCase(this string s)
        {
            return Regex.Replace(s, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToUpper(m.Value[1])}");
        }
    }
}
