using System;
using System.Collections.Generic;
using System.Text;

namespace Typerr.Model
{
    public class LibTileModel
    {
        public string Title { get; }
        public string AuthorName { get; }
        public string WebsiteName { get; }
        public int WordCount { get; }
        public int TimeRemaining { get; }

        public LibTileModel(TestModel testModel, User user)
        {
            Title = testModel.article.title;
            AuthorName = testModel.article.author;
            WebsiteName = testModel.article.site_name;
            WordCount = testModel.article.WordCount;
            TimeRemaining = WordCount / user.RecentWpm;
        }

        public LibTileModel()
        {
            Title = "Default Title";
            AuthorName = "Default Author";
            WebsiteName = "defaultwebsite.com";
            WordCount = -1;
            TimeRemaining = -1;
        }
    }
}
