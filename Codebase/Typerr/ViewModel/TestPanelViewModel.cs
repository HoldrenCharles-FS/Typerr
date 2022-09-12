using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class TestPanelViewModel : ViewModelBase
    {
        public ICommand StopTestCommand { get; }
        public ICommand PauseTestCommand { get; }
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

        public TestPanelViewModel()
        {
            StopTestCommand = new StopTestCommand(this);
            PauseTestCommand = new PauseTestCommand(this);
            Init();
        }

        private void Init()
        {
            CurrentWPM = "TBD";
        }
    }


}
