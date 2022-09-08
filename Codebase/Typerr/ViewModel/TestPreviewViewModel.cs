using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;

namespace Typerr.ViewModel
{
    public class TestPreviewViewModel : ViewModelBase
    {
        public ICommand TestPreviewCloseCommand { get; }
        public TestModel TestModel { get; }

        public User User { get; private set; }

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

        private string _summary;
        public string Summary
        {
            get
            {
                return _summary;
            }
            set
            {
                _summary = value;
                OnPropertyChanged(nameof(Summary));
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

        public TestPreviewViewModel(MainViewModel mainViewModel, TestModel testModel, User user)
        {
            TestModel = testModel;
            User = user;
            TestPreviewCloseCommand = new CreateTestCloseCommand(mainViewModel);
            Init();
            FormatWordCountAndTimeRemaining();
        }

        private void Init()
        {
            Image = TestModel.Image;
            Title = TestModel.article.title;
            Summary = TestModel.article.summary;
        }

        private void FormatWordCountAndTimeRemaining()
        {
            string wordCount = TestService.FormatNumber(TestModel.WordCount);

            string timeRemaining = TestService.FormatTimeRemaining(TestModel.WordCount, User.RecentWpm);

            string line1 = (string.IsNullOrEmpty(TestModel.article.author) && string.IsNullOrEmpty(TestModel.article.site_name))
                ? ""
                : (!string.IsNullOrEmpty(TestModel.article.author) && !string.IsNullOrEmpty(TestModel.article.site_name))
                ? "By " + TestModel.article.author + " | " + TestModel.article.site_name
                : (!string.IsNullOrEmpty(TestModel.article.author) && string.IsNullOrEmpty(TestModel.article.site_name))
                ? "By " + TestModel.article.author
                : "From " + TestModel.article.site_name;
            string pubDate = "";
            if (TestModel.article.pub_date == null)
                pubDate = "None";
            else
            { 
                DateTime date = (DateTime)TestModel.article.pub_date;

                pubDate = date.ToString("MMM dd yyyy");
            }

            FooterInfo = $"{line1} \nPublish Date: {pubDate} | {wordCount} words, {timeRemaining} remaining";
        }
    }
}
