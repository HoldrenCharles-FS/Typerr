using System;

namespace Typerr.Service
{
    public class TestService
    {
        // Calculates Word Count
        public static int GetWordCount(string txt)
        {
            // Call Format Text before calling word count
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            return txt.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string FormatText(string txt)
        {
            string text = txt;

            txt.Replace("—", string.Empty);

            while (text.Contains("  ")) text = text.Replace("  ", " ");

            return text;
        }
    }
}
