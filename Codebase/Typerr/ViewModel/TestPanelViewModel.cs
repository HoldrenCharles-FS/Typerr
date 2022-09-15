using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class TestPanelViewModel : ViewModelBase
    {
        public ICommand StopTestCommand { get; }
        public ICommand PauseTestCommand { get; }
        public TestViewModel TestVM { get; }

        private readonly User _user;

        private string _modeLabel;
        public string ModeLabel
        {
            get
            {
                return _modeLabel;
            }
            set
            {
                _modeLabel = value;
                OnPropertyChanged(nameof(ModeLabel));
            }
        }

        private string _modeData;
        public string ModeData
        {
            get
            {
                return _modeData;
            }
            set
            {
                _modeData = value;
                OnPropertyChanged(nameof(ModeData));
            }
        }

        private string _currentWPM;
        public string CurrentWPM
        {
            get
            {
                return _currentWPM;
            }
            set
            {
                _currentWPM = value;
                OnPropertyChanged(nameof(CurrentWPM));
            }
        }

        private string _timeElapsed;
        public string TimeElapsed
        {
            get
            {
                return _timeElapsed;
            }
            set
            {
                _timeElapsed = value;
                OnPropertyChanged(nameof(TimeElapsed));
            }
        }

        private int _wordsTyped;
        public int WordsTyped
        {
            get
            {
                return _wordsTyped;
            }
            set
            {
                _wordsTyped = value;
                OnPropertyChanged(nameof(WordsTyped));
            }
        }

        private int _typingErrors;
        public int TypingErrors
        {
            get
            {
                return _typingErrors;
            }
            set
            {
                _typingErrors = value;
                OnPropertyChanged(nameof(TypingErrors));
            }
        }

        private StackPanel _pausePanel;
        public StackPanel PausePanel
        {
            get
            {
                return _pausePanel;
            }
            set
            {
                _pausePanel = value;
                OnPropertyChanged(nameof(PausePanel));
            }
        }

        private bool _isPaused;
        public bool IsPaused
        {
            get
            {
                return _isPaused;
            }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
                UpdatePauseFace();
            }
        }

        private List<int> _wpmRates;

        // Timers and time data
        private static Timer _timer;
        private static Timer _updateTimer;
        public int MinutesElapsed { get; set; }
        public int SecondsElapsed { get; set; }

        // UI Elements
        public Rectangle PauseBar1 { get; private set; }
        public Rectangle PauseBar2 { get; private set; }
        public Polygon StartIcon { get; private set; }

        public TestPanelViewModel(TestViewModel testVM, User user, int wordCount, MainViewModel mainViewModel)
        {
            TestVM = testVM;
            _user = user;
            
            StopTestCommand = new StopTestCommand(this, mainViewModel);
            PauseTestCommand = new PauseTestCommand(this, testVM);
            Init(wordCount);
        }

        private void Init(int wordCount)
        {
            _wpmRates = new List<int>();

            PausePanel = new StackPanel();
            PausePanel.Orientation = Orientation.Horizontal;
            PauseBar1 = new Rectangle();
            PauseBar2 = new Rectangle();
            PauseBar1.Width = PauseBar2.Width = 10;
            PauseBar1.Height = PauseBar2.Height = 35;
            PauseBar1.Margin = PauseBar2.Margin = new Thickness(4, 0, 4, 0);
            PauseBar1.Stroke = PauseBar2.Stroke = new SolidColorBrush(Colors.White);
            PauseBar1.Fill = PauseBar2.Fill = new SolidColorBrush(Colors.White);
            PauseBar1.StrokeThickness = PauseBar2.StrokeThickness = 1.5;
            PauseBar1.Clip = PauseBar2.Clip = new RectangleGeometry(new Rect(0, 0, 10, 35), 5, 2);
            PauseBar1.IsHitTestVisible = PauseBar2.IsHitTestVisible = false;

            StartIcon = new Polygon();
            IEnumerable<Point> points = new List<Point>() {
                new Point(8,0),
                new Point(8,35),
                new Point(30,17),

            };
            StartIcon.Points = new PointCollection(points);
            StartIcon.Stroke = new SolidColorBrush(Colors.White);
            StartIcon.StrokeThickness = 1.5;
            StartIcon.Fill = new SolidColorBrush(Colors.White);
            StartIcon.Width = 35;
            StartIcon.Height = 35;
            StartIcon.IsHitTestVisible = false;
            UpdatePauseFace();

            CurrentWPM = "TBD";

            ModeLabel = (_user.Mode == 0) ? "Time Remaining" : "Words Remaining";
            ModeData = (_user.Mode == 0) ? $"{_user.Minutes}:00" : wordCount.ToString();
            TimeElapsed = "0:00";
            StartTimer();
        }

        private void StartTimer()
        {
            _timer = new Timer(60000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            
            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += OnUpdateTimedEvent;
            _updateTimer.AutoReset = true;
            _updateTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MinutesElapsed++;
            CurrentWPM = TestVM.CorrectWords.ToString();

            _wpmRates.Add(TestVM.CorrectWords);
            TestVM.ResetCorrectWords();

            if (MinutesElapsed == _user.Minutes && _user.Mode == 0)
            {
                _timer.Stop();
                _updateTimer.Stop();

                ModeData = "0:00";

                TimeElapsed = $"{_user.Minutes}:00";

                StopTestCommand.Execute(null);
            }
        }

        private void OnUpdateTimedEvent(object source, ElapsedEventArgs e)
        {
            SecondsElapsed++;

            TimeElapsed = MinutesElapsed.ToString();
            TimeElapsed += ":";

            TimeElapsed += (SecondsElapsed < 10)
                ? "0" + (SecondsElapsed % 60).ToString()
                : (SecondsElapsed % 60).ToString();

            if (_user.Mode == 0)
            {
                ModeData = (_user.Minutes - MinutesElapsed - 1).ToString();
                ModeData += ":";

                ModeData += (60 - SecondsElapsed % 60 < 10)
                    ? "0" + (60 - SecondsElapsed % 60).ToString()
                : (60 - SecondsElapsed % 60).ToString();
            }
        }

        private void UpdatePauseFace()
        {
            PausePanel.Children.Clear();

            if (_isPaused)
            {
                PausePanel.Children.Add(StartIcon);

                if (_timer != null || _updateTimer != null)
                {
                    _timer.Stop();
                    _updateTimer.Stop();
                }
                
            }
            else
            {

                PausePanel.Children.Add(PauseBar1);
                PausePanel.Children.Add(PauseBar2);

                if (_timer != null || _updateTimer != null)
                {
                    _timer.Start();
                    _updateTimer.Start();
                }

            }
        }
    }


}
