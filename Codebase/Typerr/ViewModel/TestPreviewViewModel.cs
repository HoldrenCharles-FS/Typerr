using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public enum Mode
    {
        Timed,
        Marathon
    }
    public class TestPreviewViewModel : ViewModelBase
    {
        public ICommand TestPreviewCloseCommand { get; }
        public ICommand ModeSwitchLeftCommand { get; }
        public ICommand ModeSwitchRightCommand { get; }
        public ICommand StartTestCommand { get; }
        public ICommand StartTestOverCommand { get; }
        public ICommand DeleteTestCommand { get; }
        public ICommand DeleteYesCommand { get; }
        public ICommand DeleteNoCommand { get; }
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

        private string _modeText;
        public string ModeText
        {
            get
            {
                return _modeText;
            }
            set
            {
                _modeText = value;
                ModeToolTip = ModeToolTip;
                OnPropertyChanged(nameof(ModeText));
            }
        }

        private string _modeToolTip;
        public string ModeToolTip
        {
            get
            {
                return _modeToolTip;
            }
            set
            {
                _modeToolTip = User.Mode == 0 ? "Timed test" : "No time limit"; ;
                OnPropertyChanged(nameof(ModeToolTip));
            }
        }


        private int _numericUpDownValue;
        public int NumericUpDownValue
        {
            get
            {
                return _numericUpDownValue;
            }
            set
            {
                _numericUpDownValue = value;
                OnPropertyChanged(nameof(NumericUpDownValue));
            }
        }

        private int _numericUpDownWidth;
        public int NumericUpDownWidth
        {
            get
            {
                return _numericUpDownWidth;
            }
            set
            {
                _numericUpDownWidth = value;
                OnPropertyChanged(nameof(NumericUpDownWidth));
            }
        }

        private string _startTestText;
        public string StartTestText
        {
            get
            {
                return _startTestText;
            }
            set
            {
                _startTestText = value;
                OnPropertyChanged(nameof(StartTestText));
            }
        }

        private Visibility _startTestOverVisibility;
        public Visibility StartTestOverVisibility
        {
            get
            {
                return _startTestOverVisibility;
            }
            set
            {
                _startTestOverVisibility = value;
                OnPropertyChanged(nameof(StartTestOverVisibility));
            }
        }

        private double _startTestOverWidth;
        public double StartTestOverWidth
        {
            get
            {
                return _startTestOverWidth;
            }
            set
            {
                _startTestOverWidth = value;
                OnPropertyChanged(nameof(StartTestOverWidth));
            }
        }

        public int ImageColumnWidth { get; private set; }

        public int TitleColumnWidth { get; private set; }
        public int TitleFontSize { get; private set; }

        public ObservableCollection<Button> TestPreviewControls { get; private set; }
        public ObservableCollection<TextBlock> DeleteTestControls { get; private set; }
        public TextBlock DeleteTextBlock { get; private set; }
        public TextBlock YesNoTextBlock { get; private set; }

        public TestPreviewViewModel(NavigationStore navigationStore, HomeViewModel homeViewModel, TestModel testModel, User user)
        {
            TestModel = testModel;
            User = user;
            ModeSwitchLeftCommand = new ModeSwitchLeftCommand(this);
            ModeSwitchRightCommand = new ModeSwitchRightCommand(this);
            StartTestCommand = new StartTestCommand(navigationStore, this, homeViewModel.MainViewModel, StartTestOption.Resume);
            StartTestOverCommand = new StartTestCommand(navigationStore, this, homeViewModel.MainViewModel, StartTestOption.Start);
            DeleteTestCommand = new DeleteTestCommand(this);
            DeleteYesCommand = new DeleteYesCommand(this, homeViewModel);
            DeleteNoCommand = new DeleteNoCommand(this);
            TestPreviewCloseCommand = new DialogCloseCommand(homeViewModel.MainViewModel);
            TestPreviewControls = new ObservableCollection<Button>();
            DeleteTestControls = new ObservableCollection<TextBlock>();
            Init();
            FormatWordCountAndTimeRemaining();
        }

        private void Init()
        {
            ModeToolTip = ModeToolTip;
            Image = TestModel.Image;
            Title = TestModel.article.title;
            Summary = TestModel.article.summary;
            ModeText = FormatService.GetMode(User.Mode);
            NumericUpDownValue = (User.Mode == 0)
                ? User.Minutes : 3;
            NumericUpDownWidth = (User.Mode == 0)
                ? 60 : 0;
            ImageColumnWidth = (Image == null) ? 15 : 300;
            TitleColumnWidth = (Image == null) ? 775 : 369;
            TitleFontSize = (Image == null) ? 28 : 24;
            StartTestText = TestModel.testData.TestStarted ? "Resume Test" : "Start Test";
            StartTestOverVisibility = TestModel.testData.TestStarted ? Visibility.Visible : Visibility.Hidden;
            StartTestOverWidth = TestModel.testData.TestStarted ? double.NaN : 0;

            Button startButton = new Button();
            startButton.Style = Application.Current.TryFindResource("PrimaryButtonTheme") as Style;
            startButton.Content = "Start Test";
            startButton.Command = StartTestCommand;

            if (TestModel.testData.TestStarted)
            {
                Button startTestOverButton = new Button();
                startTestOverButton.Style = Application.Current.TryFindResource("SecondaryButtonTheme") as Style;
                Style startTestOverButtonStyle = new Style(typeof(Border));
                startTestOverButton.Resources.Add(new Key(), startTestOverButtonStyle);
                startTestOverButton.Content = "Start Test Over"; 

                startButton.Content = "Resume Test";
                startButton.Margin = new Thickness(10, 0, 0, 0);

                TestPreviewControls.Add(startTestOverButton);
                TestPreviewControls.Add(startButton);
            }
            else
            {
                TestPreviewControls.Add(startButton);
            }

            DeleteTextBlock = new TextBlock();
            DeleteTextBlock.Padding = new Thickness(35, 15, 0, 15);
            DeleteTextBlock.FontSize = 16;
            Hyperlink deleteHyperlink = new Hyperlink(new Run("Delete test"));
            deleteHyperlink.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 68, 51));
            deleteHyperlink.Command = DeleteTestCommand;

            DeleteTextBlock.Inlines.Clear();
            DeleteTextBlock.Inlines.Add(deleteHyperlink);

            DeleteTestControls.Add(DeleteTextBlock);

            YesNoTextBlock = new TextBlock();
            YesNoTextBlock.Padding = DeleteTextBlock.Padding;
            YesNoTextBlock.FontSize = DeleteTextBlock.FontSize;
            Run areYouSure = new Run("Are you sure: ");
            Hyperlink yes = new Hyperlink(new Run("Yes"));
            Run slash = new Run(" / ");
            Hyperlink no = new Hyperlink(new Run("No"));

            yes.Command = DeleteYesCommand;
            no.Command = DeleteNoCommand;

            YesNoTextBlock.Inlines.Clear();
            YesNoTextBlock.Inlines.Add(areYouSure);
            YesNoTextBlock.Inlines.Add(yes);
            YesNoTextBlock.Inlines.Add(slash);
            YesNoTextBlock.Inlines.Add(no);
        }

        private void FormatWordCountAndTimeRemaining()
        {
            string wordCount = FormatService.FormatNumber(TestModel.WordCount);

            string timeRemaining = FormatService.FormatTimeRemaining(TestModel.WordCount, User.RecentWpm);

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

                pubDate = date.ToString("MMMM dd yyyy");
            }

            FooterInfo = $"{line1}\nPublish Date: {pubDate}\n{wordCount} words, {timeRemaining} remaining";
        }
    }
}
