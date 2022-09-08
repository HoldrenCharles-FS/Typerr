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
            txt.Replace("--", string.Empty);

            while (text.Contains("  ")) text = text.Replace("  ", " ");

            return text;
        }

        public static string FormatTimeRemaining(int wordCount, int wpm) 
        {
            string time = "";

            int timeRemaining = wordCount / wpm;

            if (timeRemaining < 1 && timeRemaining > 0)
            {
                time = timeRemaining * 60 + "s";
            }
            else if (timeRemaining > 43830)
            {
                time = Math.Round((double)timeRemaining / 43830, 1) + "mo";
            }
            else if (timeRemaining > 1440)
            {
                time = Math.Round((double)timeRemaining / 1440, 1) + "d";
            }
            else if (timeRemaining > 60)
            {
                time = Math.Round((double)timeRemaining / 60, 1) + "h";
            }
            else
            {
                time = timeRemaining + "m";
            }


            return time;
        }

        public static string FormatNumber(int num)
        {
            string text = num.ToString();

            for (int i = text.Length, j = 0; i > 0; i--, j++)
            {
                if (j % 3 == 0 && j != 0)
                {
                    text = text.Insert(i, ",");
                }
            }

            return text;
        }
    }
}
