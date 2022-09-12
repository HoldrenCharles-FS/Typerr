using System;
using System.Collections.Generic;
using System.Text;
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

        public Rectangle PauseBar1 { get; private set; }
        public Rectangle PauseBar2 { get; private set; }
        public Polygon StartIcon { get; private set; }

        public TestPanelViewModel(User user)
        {
            _user = user;
            StopTestCommand = new StopTestCommand(this);
            PauseTestCommand = new PauseTestCommand(this);
            Init();
        }

        private void Init()
        {
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
        }

        private void UpdatePauseFace()
        {
            PausePanel.Children.Clear();

            if (_isPaused)
            {
                PausePanel.Children.Add(StartIcon);
            }
            else
            {

                    PausePanel.Children.Add(PauseBar1);
                    PausePanel.Children.Add(PauseBar2);
                
            }
        }
    }


}
