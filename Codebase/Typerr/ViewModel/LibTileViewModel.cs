using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.ViewModel;

namespace Typerr.View
{
    public class LibTileViewModel : ViewModelBase
    {
        public TestModel Model { get; private set; }
        public User User { get; private set; }
        public string Title => Model.article.title;
        public string AuthorName => Model.article.author;
        public string WebsiteName => Model.article.site_name;
        public string WordCount { get; private set; }
        public string TimeRemaining { get; private set; }

        public LibTileViewModel(TestModel model, User user)
        {
            Model = model;
            User = user;
            FormatWordCountAndTimeRemaining();
        }

        public LibTileViewModel()
        {
            Model = new TestModel();
            User = new User(33);
            FormatWordCountAndTimeRemaining();
        }

        private void FormatWordCountAndTimeRemaining()
        {
            string wordCount = Model.WordCount.ToString();

            for (int i = wordCount.Length, j = 0; i > 0; i--, j++)
            {
                if (j % 3 == 0 && j != 0)
                {
                    wordCount = wordCount.Insert(i, ",");
                }
            }

            WordCount = wordCount;

            int timeRemaining = Model.WordCount / User.RecentWpm; ;

            if (timeRemaining < 1 && timeRemaining > 0)
            {
                TimeRemaining = timeRemaining * 60 + "s";
            }
            else if (timeRemaining > 43830) 
            {
                TimeRemaining = Math.Round((double)timeRemaining / 43830, 1) + "mo";
            }
            else if (timeRemaining > 1440)
            {
                TimeRemaining = Math.Round((double)timeRemaining / 1440, 1) + "d";
            }
            else if (timeRemaining > 60)
            {
                TimeRemaining = Math.Round((double)timeRemaining / 60, 1) + "h";
            }
            else
            {
                TimeRemaining = timeRemaining + "m";
            }
        }
    }
}
