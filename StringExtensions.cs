using System.Globalization;

namespace AnalisardorCartao
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, CompareOptions comp)
        {
            if (!string.IsNullOrEmpty(source))
            {
                return CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, toCheck, comp) >= 0;
            }
            return false;
        }
    }
}
