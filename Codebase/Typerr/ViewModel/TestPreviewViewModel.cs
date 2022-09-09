﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;

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
                OnPropertyChanged(nameof(ModeText));
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

        private Visibility _numericUpDownVisibility;
        public Visibility NumericUpDownVisibility
        {
            get
            {
                return _numericUpDownVisibility;
            }
            set
            {
                _numericUpDownVisibility = value;
                OnPropertyChanged(nameof(NumericUpDownVisibility));
            }
        }

        public ObservableCollection<Button> TestPreviewControls { get; private set; }

        public TestPreviewViewModel(MainViewModel mainViewModel, TestModel testModel, User user)
        {
            TestModel = testModel;
            User = user;
            ModeSwitchLeftCommand = new ModeSwitchLeftCommand(this);
            ModeSwitchRightCommand = new ModeSwitchRightCommand(this);
            StartTestCommand = new StartTestCommand(this);
            TestPreviewCloseCommand = new DialogCloseCommand(mainViewModel);
            TestPreviewControls = new ObservableCollection<Button>();
            Init();
            FormatWordCountAndTimeRemaining();
        }

        private void Init()
        {
            Image = TestModel.Image;
            Title = TestModel.article.title;
            Summary = TestModel.article.summary;
            ModeText = TestService.GetMode(User.Mode);
            NumericUpDownValue = (User.Mode == 0)
                ? User.Minutes : 3;
            NumericUpDownWidth = (User.Mode == 0)
                ? 60 : 0;

            Button startButton = new Button();
            Style startButtonStyle = new Style(typeof(Border));
            startButtonStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, 30));
            startButton.Resources.Add(new Key(), startButtonStyle);
            startButton.Content = "Start Test";
            startButton.Command = StartTestCommand;

            if (TestModel.testData.TestStarted)
            {
                Button startTestOverButton = new Button();
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

                pubDate = date.ToString("MMMM dd yyyy");
            }

            FooterInfo = $"{line1}\nPublish Date: {pubDate}\n{wordCount} words, {timeRemaining} remaining";
        }
    }
}