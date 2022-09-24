using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Model;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly User _user;

        private ObservableCollection<LibTileViewModel> _libTileViewModels;

        public ObservableCollection<LibTileViewModel> LibTileViewModels
        {
            get { return _libTileViewModels; }
            set { _libTileViewModels = value; }
        }

        private ObservableCollection<FeedTileViewModel> _feedTileViewModels;

        public ObservableCollection<FeedTileViewModel> FeedTileViewModels
        {
            get { return _feedTileViewModels; }
            set { _feedTileViewModels = value; }
        }

        private ObservableCollection<SubTileViewModel> _subTileViewModels;

        public ObservableCollection<SubTileViewModel> SubTileViewModels
        {
            get { return _subTileViewModels; }
            set { _subTileViewModels = value; }
        }

        public MainViewModel MainViewModel { get; }
        public NavPanelViewModel NavPanelViewModel { get; set; }
        public ICommand CreateTestTileCommand { get; }
        public ICommand AddSubscriptionTileCommand { get; }
        public ICommand GoToLibraryCommand { get; }
        public ICommand GoToSubscriptionsCommand { get; }

        private double _feedContentHeight;
        public double FeedContentHeight
        {
            get
            {
                return _feedContentHeight;
            }
            set
            {
                _feedContentHeight = value;
                OnPropertyChanged(nameof(FeedContentHeight));
            }
        }


        public HomeViewModel(MainViewModel mainViewModel, ICommand createTestTileCommand, ICommand goToLibraryCommand, ICommand goToSubscriptionsCommand, User user)
        {
            MainViewModel = mainViewModel;
            CreateTestTileCommand = createTestTileCommand;
            GoToLibraryCommand = goToLibraryCommand;
            GoToSubscriptionsCommand = goToSubscriptionsCommand;
            AddSubscriptionTileCommand = new AddSubscriptionTileCommand(mainViewModel);
            _libTileViewModels = new ObservableCollection<LibTileViewModel>();
            _feedTileViewModels = new ObservableCollection<FeedTileViewModel>();
            _subTileViewModels = new ObservableCollection<SubTileViewModel>();
            _user = user;
            Init();
        }

        private void Init()
        {
            FeedContentHeight = 0;
            RefreshLibrary();
        }

        public void RefreshLibrary()
        {
            IEnumerable<LibTileViewModel> result = MainViewModel.AllLibTileViewModels.Take(6);

            LibTileViewModels.Clear();

            foreach (var r in result)
            {
                LibTileViewModels.Add(r);
            }
        }

        public void RefreshSubscriptions()
        {
            IEnumerable<SubTileViewModel> result = MainViewModel.AllSubTileViewModels.Take(8);

            SubTileViewModels.Clear();

            foreach (var r in result)
            {
                SubTileViewModels.Add(r);
            }
        }

        public void RefreshFeed()
        {
            IEnumerable<FeedTileViewModel> result = MainViewModel.AllFeedTileViewModels.Take(12);

            FeedTileViewModels.Clear();

            foreach (var r in result)
            {
                FeedTileViewModels.Add(r);
            }
        }
    }
}