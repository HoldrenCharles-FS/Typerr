using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;
using MahApps.Metro.IconPacks;
using Typerr.Stores;
using System.Windows.Media;
using System.Windows;

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

        private double _starOffset;
        public double StarOffset
        {
            get
            {
                return _starOffset;
            }
            set
            {
                _starOffset = value;
                OnPropertyChanged(nameof(StarOffset));
            }
        }

        private PackIconBootstrapIcons _star1;
        public PackIconBootstrapIcons Star1
        {
            get
            {
                return _star1;
            }
            set
            {
                _star1 = value;
                OnPropertyChanged(nameof(Star1));
            }
        }

        private PackIconBootstrapIcons _star2;
        public PackIconBootstrapIcons Star2
        {
            get
            {
                return _star2;
            }
            set
            {
                _star2 = value;
                OnPropertyChanged(nameof(Star2));
            }
        }

        private PackIconBootstrapIcons _star3;
        public PackIconBootstrapIcons Star3
        {
            get
            {
                return _star3;
            }
            set
            {
                _star3 = value;
                OnPropertyChanged(nameof(Star3));
            }
        }

        public ResultsViewModel(TestPanelViewModel testPanelViewModel, TestViewModel testViewModel, MainViewModel mainViewModel)
        {
            _testPanelViewModel = testPanelViewModel;
            _testViewModel = testViewModel;
            StartTestOverCommand = new StartTestOverCommand(mainViewModel, testViewModel);
            ReturnToMenuCommand = new ReturnToMenuCommand(mainViewModel);
            Init();
        }

        private void Init()
        {
            Lowest = _testPanelViewModel.GetLowest();
            Highest = _testPanelViewModel.GetHighest();
            Average = (int)_testPanelViewModel.GetAverage();
            double accuracy = Math.Round((double)_testViewModel.CorrectWordsTotal / _testPanelViewModel.WordsTyped * 100, 1);
            Accuracy = "Accuracy: " + (double.IsNaN(accuracy) ? 0 : accuracy) + "%";

            StarOffset = 20;
            Star1 = new PackIconBootstrapIcons();
            Star2 = new PackIconBootstrapIcons();
            Star3 = new PackIconBootstrapIcons();

            Color starColor = Color.FromArgb(255, 255, 238, 0);

            Star1.Foreground = new SolidColorBrush(starColor);
            Star2.Foreground = new SolidColorBrush(starColor);
            Star3.Foreground = new SolidColorBrush(starColor);

            const double starSize = 50;

            Star1.Width = starSize;
            Star1.Height = starSize;
            Star2.Width = starSize;
            Star2.Height = starSize;
            Star3.Width = starSize;
            Star3.Height = starSize;

            double rating = accuracy - _testViewModel.ErrorPositions.Count;

            // 3 stars
            if (rating >= 95)
            {
                Star1.Kind = PackIconBootstrapIconsKind.StarFill;
                Star2.Kind = PackIconBootstrapIconsKind.StarFill;
                Star3.Kind = PackIconBootstrapIconsKind.StarFill;
            }
            // 2.5 stars
            else if (rating >= 90)
            {
                Star1.Kind = PackIconBootstrapIconsKind.StarFill;
                Star2.Kind = PackIconBootstrapIconsKind.StarFill;
                Star3.Kind = PackIconBootstrapIconsKind.StarHalf;
            }
            // 2 stars
            else if (rating >= 85)
            {
                Star1.Kind = PackIconBootstrapIconsKind.StarFill;
                Star2.Kind = PackIconBootstrapIconsKind.StarFill;
                Star3.Kind = PackIconBootstrapIconsKind.Star;
            }
            // 1.5 stars
            else if (rating >= 80)
            {
                Star1.Kind = PackIconBootstrapIconsKind.StarFill;
                Star2.Kind = PackIconBootstrapIconsKind.StarHalf;
                Star3.Kind = PackIconBootstrapIconsKind.Star;
            }
            // 1 star
            else if (rating >= 75)
            {
                Star1.Kind = PackIconBootstrapIconsKind.StarFill;
                Star2.Kind = PackIconBootstrapIconsKind.Star;
                Star3.Kind = PackIconBootstrapIconsKind.Star;
            }
            // 0 stars
            else
            {
                Star1.Kind = PackIconBootstrapIconsKind.Star;
                Star2.Kind = PackIconBootstrapIconsKind.Star;
                Star3.Kind = PackIconBootstrapIconsKind.Star;
            }

        }
    }
}
