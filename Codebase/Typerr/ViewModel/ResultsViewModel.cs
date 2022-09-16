using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class ResultsViewModel : ViewModelBase
    {
        private readonly TestPanelViewModel _testPanelViewModel;
        private readonly TestViewModel _testViewModel;

        public ICommand StartTestOverCommand { get; }
        public ICommand ReturnToMenuCommand { get; }

        private int _lowest;
        public int Lowest
        {
            get
            {
                return _lowest;
            }
            set
            {
                _lowest = value;
                OnPropertyChanged(nameof(Lowest));
            }
        }

        private int _highest;
        public int Highest
        {
            get
            {
                return _highest;
            }
            set
            {
                _highest = value;
                OnPropertyChanged(nameof(Highest));
            }
        }

        private int _average;
        public int Average
        {
            get
            {
                return _average;
            }
            set
            {
                _average = value;
                OnPropertyChanged(nameof(Average));
            }
        }

        private string _accuracy;

        public string Accuracy
        {
            get
            {
                return _accuracy;
            }
            set
            {
                _accuracy = value;
                OnPropertyChanged(nameof(Accuracy));
            }
        }

        public ResultsViewModel(TestPanelViewModel testPanelViewModel, TestViewModel testViewModel, MainViewModel mainViewModel)
        {
            _testPanelViewModel = testPanelViewModel;
            _testViewModel = testViewModel;
            StartTestOverCommand = new StartTestOverCommand(this, mainViewModel, testViewModel);
            Init();
        }

        private void Init()
        {
            Lowest = _testPanelViewModel.GetLowest();
            Highest = _testPanelViewModel.GetHighest();
            Average = (int)_testPanelViewModel.GetAverage();
            double accuracy = Math.Round((double)_testViewModel.CorrectWordsTotal / _testPanelViewModel.WordsTyped * 100, 1);
            Accuracy = "Accuracy: " + accuracy + "%";
        }
    }
}
