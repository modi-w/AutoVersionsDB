namespace AutoVersionsDB.Helpers
{
    public static class StringExtensions
    {
        public static string ToTrimedInvariant(this string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                return str.Trim().ToUpperInvariant();
            }
        }

        public static string Ellipsis(this string str, int maxLength)
        {
            str.ThrowIfNull(nameof(str));

            string resultsStr = str;

            if (str.Length > maxLength)
            {
                resultsStr = $"{str.Substring(0, maxLength - 3)}...";
            }

            return resultsStr;
        }
    }
}
