using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Typerr.Model
{
    public class TestModel
    {
        public Article article { get; set; }
        public TestData testData { get; private set; }

        // Additional Fields
        private readonly DateTime _creationDate;
        public BitmapImage Image { get; set; }
        public BitmapImage Favicon { get; set; }
        public int WordCount { get; set; }

        public TestModel()
        {
            article = new Article();
            testData = new TestData();
            _creationDate = DateTime.Now;
        }

        //public TestModel(Article article, TestData testData)
        //{
        //    this.article = article;
        //    this.testData = testData;
        //}

        

        //public TestModel(string title, string text, string summary, string author, string site_name, string canonical_url)
        //{
        //    article.title = title;
        //    article.text = text;
        //    article.summary = summary;
        //    article.author = author;
        //    article.site_name = site_name;
        //    article.canonical_url = canonical_url;
        //    article.pub_date = null;
        //    article.image = null;
        //    article.favicon = null;
        //}

       
    }
}
