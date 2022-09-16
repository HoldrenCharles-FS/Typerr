using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;
using Typerr.Model;

namespace Typerr.Service
{
    public class TestService
    {
        public static void Write(TestModel testModel, string filename = null)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = testModel.Filename;
            }
            else
            {
                // Assign the newly created tests filename to the paramater passed in
                testModel.Filename = filename;
            }

            try
            {
                File.Delete(filename);
            }
            catch (Exception)
            {
                throw;
            }

            

            string errorPositions = "NULL";

            if (testModel.testData.ErrorPositions.Count == 1)
            {
                errorPositions = testModel.testData.ErrorPositions[0].ToString();
            }
            else if (testModel.testData.ErrorPositions.Count > 0)
            {
                errorPositions = "";
                for (int i = 0; i < testModel.testData.ErrorPositions.Count; i++)
                {
                    errorPositions += testModel.testData.ErrorPositions[i].ToString();
                    if (i != testModel.testData.ErrorPositions.Count - 1)
                    {
                        errorPositions += ",";
                    }
                }
            }

            FileStream writer = new FileStream(filename, FileMode.CreateNew);

            using (XmlWriter xmlWriter = XmlWriter.Create(writer))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("TestModel");
                xmlWriter.WriteAttributeString("Image", string.IsNullOrWhiteSpace(testModel.Base64Image) ? "NULL" : testModel.Base64Image);
                xmlWriter.WriteStartElement("article");
                xmlWriter.WriteAttributeString("title", testModel.article.title);
                xmlWriter.WriteAttributeString("text", testModel.article.text);
                xmlWriter.WriteAttributeString("summary", testModel.article.summary);
                xmlWriter.WriteAttributeString("author", testModel.article.author);
                xmlWriter.WriteAttributeString("site_name", testModel.article.site_name);
                xmlWriter.WriteAttributeString("canonical_url", testModel.article.canonical_url);
                xmlWriter.WriteAttributeString("pub_date", testModel.article.pub_date.ToString());
                xmlWriter.WriteAttributeString("image", testModel.article.image);
                xmlWriter.WriteAttributeString("favicon", testModel.article.favicon);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("testData");
                xmlWriter.WriteAttributeString("TestStarted", testModel.testData.TestStarted.ToString());
                xmlWriter.WriteAttributeString("LastPosition", testModel.testData.LastPosition.ToString());
                xmlWriter.WriteAttributeString("ErrorPositions", errorPositions);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();
        }

        // Calculates Word Count
        public static int GetWordCount(string txt)
        {
            // Call Format Text before calling word count
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            return txt.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string FormatText(string txt)
        {
            txt = txt.Replace("—", " ");
            txt = txt.Replace("--", " ");
            txt = txt.Replace("---", " ");
            txt = txt.Replace("\n", " ");
            txt = txt.Replace("\r", " ");
            txt = txt.Replace("\r", " ");
            txt = txt.Replace("&#xA;", " ");
            txt = txt.Replace("&quot;", " ");

            while (txt.Contains(" .")) txt = txt.Replace(" .", ".");
            while (txt.Contains(" ,")) txt = txt.Replace(" ,", ",");
            while (txt.Contains(" !")) txt = txt.Replace(" !", "!");
            while (txt.Contains(" '")) txt = txt.Replace(" '", "'");

            while (txt.Contains("  ")) txt = txt.Replace("  ", " ");

            return txt;
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

        public static string GetMode(int mode)
        {
            string text = "";
            switch (mode)
            {
                case 0:
                    text = "MINUTES";
                    break;
                case 1:
                    text = "MARATHON";
                    break;
            }
            return text;
        }
    }
}
