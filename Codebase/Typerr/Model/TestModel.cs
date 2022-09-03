using System;
using System.Collections.Generic;
using System.Text;

namespace Typerr.Model
{
    public class TestModel
    {
        public Article article { get; set; }
        public TestData testData { get; private set; }

        public TestModel()
        {
            testData = new TestData();
        }

        public TestModel(Article article, TestData testData)
        {
            this.article = article;
            this.testData = testData;
        }

        // Additional Fields
        private readonly string _requestURL;
        private readonly DateTime _creationDate;
        public dynamic RssImage { get; set; }
        public int WordCount { get; }

        public TestModel(string title, string text, string summary, string author, string site_name, string canonical_url)
        {
            article.title = title;
            article.text = text;
            article.summary = summary;
            article.author = author;
            article.site_name = site_name;
            article.canonical_url = canonical_url;
            article.pub_date = DateTime.MinValue;
            article.image = null;
            article.favicon = null;
        }

        // Calculates Word Count
        private int GetWordCount()
        {
            // We need to format the text beforehand
            FormatText();

            int wordCount = 0;
            bool spaceChecked = false;

            for (int i = 0; i < article.text.Length; i++)
            {
                if (article.text[i] == ' ' && !spaceChecked)
                {
                    spaceChecked = true;
                    wordCount++;
                }
                else
                {
                    spaceChecked = false;
                }
            }

            return wordCount;
        }

        private void FormatText()
        {
            //TODO: Format text
        }
    }
}
