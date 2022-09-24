using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public User User { get; }

        public HomeViewModel HomeViewModel { get; private set; }
        public NavPanelViewModel NavPanelViewModel { get; private set; }

        private readonly ObservableCollection<LibTileViewModel> _allLibTileViewModels;

        public IEnumerable<LibTileViewModel> AllLibTileViewModels => _allLibTileViewModels;

        private readonly ObservableCollection<FeedTileViewModel> _allFeedTileViewModels;

        public IEnumerable<FeedTileViewModel> AllFeedTileViewModels => _allFeedTileViewModels;

        private readonly ObservableCollection<SubTileViewModel> _allSubTileViewModels;

        public IEnumerable<SubTileViewModel> AllSubTileViewModels => _allSubTileViewModels;

        private ViewModelBase _currentDialog;
        public ViewModelBase CurrentDialog
        {
            get
            {
                return _currentDialog;
            }
            set
            {
                _currentDialog = value;
                OnPropertyChanged(nameof(CurrentDialog));
            }
        }

        private ViewModelBase _currentPanel;
        public ViewModelBase CurrentPanel
        {
            get
            {
                return _currentPanel;
            }
            set
            {
                _currentPanel = value;
                OnPropertyChanged(nameof(CurrentPanel));
            }
        }

        private Visibility _overlayVisibility;
        public Visibility OverlayVisibility
        {
            get
            {
                return _overlayVisibility;
            }
            set
            {
                _overlayVisibility = value;
                OnPropertyChanged(nameof(OverlayVisibility));
            }
        }

        private CreateTestViewModel _createTestViewModel;
        public CreateTestViewModel CreateTestViewModel
        {
            get
            {
                return _createTestViewModel;
            }
            set
            {
                _createTestViewModel = value;
                OnPropertyChanged(nameof(CreateTestViewModel));
            }
        }


        public MainViewModel(NavigationStore navigationStore, User user)
        {
            OverlayVisibility = Visibility.Collapsed;
            _navigationStore = navigationStore;
            User = user;
            _allLibTileViewModels = new ObservableCollection<LibTileViewModel>();
            _allFeedTileViewModels = new ObservableCollection<FeedTileViewModel>();
            _allSubTileViewModels = new ObservableCollection<SubTileViewModel>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void AddLibTile(TestModel testModel, HomeViewModel homeViewModel)
        {
            _allLibTileViewModels.Insert(0, new LibTileViewModel(_navigationStore, homeViewModel, testModel, User));
        }

        public void RemoveLibTile(string filename)
        {
            foreach (LibTileViewModel libTile in AllLibTileViewModels)
            {
                if (libTile.TestModel.Filename == filename)
                {
                    _allLibTileViewModels.Remove(libTile);
                    break;
                }
            }
        }

        public void UpdateLibTile(string filename)
        {
            foreach (LibTileViewModel libTile in AllLibTileViewModels)
            {
                if (libTile.TestModel.Filename == filename)
                {
                    _allLibTileViewModels.Insert(_allLibTileViewModels.IndexOf(libTile), LoadTest(filename));
                    _allLibTileViewModels.Remove(libTile);
                    HomeViewModel.RefreshLibrary();
                    break;
                }
            }
        }

        public void AddFeedTile(ISyndicationItem syndicationItem, string source)
        {
            _allFeedTileViewModels.Insert(0, new FeedTileViewModel(syndicationItem, source, this));
        }

        public void ClearFeedTiles()
        {
            _allFeedTileViewModels.Clear();
        }

        public void AddSubTile(RssModel rssModel)
        {
            _allSubTileViewModels.Insert(0, new SubTileViewModel(rssModel));
        }

        public void ClearSubTiles()
        {
            _allFeedTileViewModels.Clear();
        }

        private LibTileViewModel LoadTest(string filename)
        {
            return new LibTileViewModel(_navigationStore, HomeViewModel, TestService.Read(filename), User);
        }

        public void SetCurrentView(ViewModelBase viewModelBase)
        {
            _navigationStore.CurrentViewModel = viewModelBase;
        }
        
        public void SetHomeViewModel(HomeViewModel homeViewModel)
        {
            HomeViewModel = homeViewModel;
        }
        public void SetNavPanelViewModel(NavPanelViewModel navPanelViewModel)
        {
            NavPanelViewModel = navPanelViewModel;
        }

    }
}
