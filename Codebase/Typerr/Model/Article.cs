using System;

namespace Typerr.Model
{
    public class Article
    {
        // Article object from API
        public string title { get; set; }
        public string text { get; set; }
        public string summary { get; set; }
        public string author { get; set; }
        public string site_name { get; set; }
        public string canonical_url { get; set; }
        public DateTime pub_date { get; set; }
        public dynamic image { get; set; }
        public dynamic favicon { get; set; }

        
        // Additional Fields
        private readonly string _requestURL;
        private readonly DateTime _creationDate;
        public dynamic RssImage { get; set; }
        public int WordCount { get; }

        public Article(string requestUrl)
        {
            _requestURL = requestUrl;
            _creationDate = DateTime.Now;
           // WordCount = GetWordCount();
        }

        public Article()
        {
            title = "";
            text = "";
            summary = "";
            author = "";
            site_name = "";
            canonical_url = "";
            pub_date = DateTime.MinValue;
            image = null;
            favicon = null;

        }

        public Article(string title, string text, string summary, string author, string site_name, string canonical_url)
        {
            this.title = title;
            this.text = text;
            this.summary = summary;
            this.author = author;
            this.site_name = site_name;
            this.canonical_url = canonical_url;
            pub_date = DateTime.MinValue;
            image = null;
            favicon = null;
        }

        // Calculates Word Count
        private int GetWordCount()
        {
            // We need to format the text beforehand
            FormatText();

            int wordCount = 0;
            bool spaceChecked = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ' && !spaceChecked)
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
