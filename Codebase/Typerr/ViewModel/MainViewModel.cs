using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Stores;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private readonly User _user;

        private readonly ObservableCollection<LibTileViewModel> _allLibTileViewModels;

        public IEnumerable<LibTileViewModel> AllLibTileViewModels => _allLibTileViewModels;

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

        public NavPanelViewModel NavPanelViewModel { get; set; }

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
            _user = user;
            _allLibTileViewModels = new ObservableCollection<LibTileViewModel>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void AddLibTile(TestModel testModel, HomeViewModel homeViewModel)
        {
            LibTileViewModel libTileViewModel = new LibTileViewModel(_navigationStore, homeViewModel, testModel, _user);

            _allLibTileViewModels.Insert(0, libTileViewModel);
        }

        public void RemoveLibTile(FileInfo fileInfo)
        {
            foreach (LibTileViewModel libTile in AllLibTileViewModels)
            {
                if (libTile.TestModel.FileName == fileInfo)
                {
                    _allLibTileViewModels.Remove(libTile);
                    break;
                }
            }
        }

    }
}
