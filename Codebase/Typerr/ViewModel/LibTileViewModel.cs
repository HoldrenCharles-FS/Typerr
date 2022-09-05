using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Typerr.Model;
using Typerr.ViewModel;

namespace Typerr.View
{
    public class LibTileViewModel : ViewModelBase
    {
        public TestModel Model { get; private set; }
        public User User { get; private set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _authorName;
        public string AuthorName
        {
            get
            {
                return _authorName;
            }
            set
            {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        private string _websiteName;
        public string WebsiteName
        {
            get
            {
                return _websiteName;
            }
            set
            {
                _websiteName = value;
                OnPropertyChanged(nameof(WebsiteName));
            }
        }

        private string _wordCount;
        public string WordCount
        {
            get
            {
                return _wordCount;
            }
            set
            {
                _wordCount = value;
                OnPropertyChanged(nameof(WordCount));
            }
        }

        private string _timeRemaining;
        public string TimeRemaining
        {
            get
            {
                return _timeRemaining;
            }
            set
            {
                _timeRemaining = value;
                OnPropertyChanged(nameof(TimeRemaining));
            }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        //public string Title => Model.article.title;
        //public string AuthorName => Model.article.author;
        //public string WebsiteName => Model.article.site_name;
        //public string WordCount { get; private set; }
        //public string TimeRemaining { get; private set; }

        private string _footerInfo;
        public string FooterInfo
        {
            get
            {
                return _footerInfo;
            }
            set
            {
                _footerInfo = value;
                OnPropertyChanged(nameof(FooterInfo));
            }
        }

        public LibTileViewModel(TestModel model, User user)
        {
            Model = model;
            User = user;
            Init();
            
        }

        private void Init()
        {
            Title = Model.article.title;
            AuthorName = Model.article.author;
            WebsiteName = Model.article.site_name;
            Image = Model.Image;
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

            string line1 = ((string.IsNullOrEmpty(AuthorName) && string.IsNullOrEmpty(WebsiteName))
                ? ""
                : (!string.IsNullOrEmpty(AuthorName) && !string.IsNullOrEmpty(WebsiteName))
                ? AuthorName + " | " + WebsiteName
                : (!string.IsNullOrEmpty(AuthorName) && string.IsNullOrEmpty(WebsiteName))
                ? AuthorName
                : WebsiteName);

            FooterInfo = $"{line1} \n{WordCount} words | {TimeRemaining} remaining";

        }
    }
}
