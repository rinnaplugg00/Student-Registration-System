using System.Globalization;
using System.Text;

namespace StudentRegistrationSystem
{
    /// <summary>
    /// Убирает BOM, zero-width и прочие непечатаемые символы, из-за которых в DataGridView возле текста появляются «квадратики».
    /// </summary>
    internal static class DisplayTextHelper
    {
        public static string SanitizeNameForGrid(object value)
        {
            if (value == null)
                return string.Empty;
            return SanitizeNameForGrid(value.ToString());
        }

        public static string SanitizeNameForGrid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            var sb = new StringBuilder(s.Length);
            foreach (char c in s.Trim())
            {
                if (IsInvisibleOrFormatOnly(c))
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        private static bool IsInvisibleOrFormatOnly(char c)
        {
            switch (c)
            {
                case '\uFEFF': // BOM
                case '\u200B': // zero-width space
                case '\u200C':
                case '\u200D':
                case '\u2060': // word joiner
                case '\u00AD': // soft hyphen (часто даёт «квадрат» при отсутствии переноса)
                    return true;
            }

            return char.GetUnicodeCategory(c) == UnicodeCategory.Format;
        }
    }
}
