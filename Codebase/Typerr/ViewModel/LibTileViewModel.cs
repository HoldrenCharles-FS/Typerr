using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.View
{
    public class LibTileViewModel : ViewModelBase
    {
        public LibTileCommand LibTileCommand { get; }

        private readonly NavigationStore _navigationStore;

        public TestModel TestModel { get; private set; }
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

        public LibTileViewModel(NavigationStore navigationStore, HomeViewModel homeViewModel, TestModel testModel, User user)
        {
            _navigationStore = navigationStore;
            TestModel = testModel;
            User = user;
            LibTileCommand = new LibTileCommand(homeViewModel.MainViewModel, new TestPreviewViewModel(_navigationStore, homeViewModel, testModel, user));
            Init();
            
        }

        private void Init()
        {
            Title = TestModel.article.title;
            AuthorName = TestModel.article.author;
            WebsiteName = TestModel.article.site_name;
            Image = TestModel.Image;
            FormatWordCountAndTimeRemaining();
        }

        private void FormatWordCountAndTimeRemaining()
        {
            WordCount = TestService.FormatNumber(TestModel.WordCount);

            TimeRemaining = TestService.FormatTimeRemaining(TestModel.WordCount, User.RecentWpm);

            string line1 = (string.IsNullOrEmpty(AuthorName) && string.IsNullOrEmpty(WebsiteName))
                ? ""
                : (!string.IsNullOrEmpty(AuthorName) && !string.IsNullOrEmpty(WebsiteName))
                ? AuthorName + " | " + WebsiteName
                : (!string.IsNullOrEmpty(AuthorName) && string.IsNullOrEmpty(WebsiteName))
                ? AuthorName
                : WebsiteName;

            FooterInfo = $"{line1} \n{WordCount} words | {TimeRemaining} remaining";
        }
    }
}
