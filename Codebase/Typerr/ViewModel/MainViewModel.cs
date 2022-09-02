using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private Visibility _overlayBarVisibility;
        public Visibility OverlayBarVisibility
        {
            get
            {
                return _overlayBarVisibility;
            }
            set
            {
                _overlayBarVisibility = value;
                OnPropertyChanged(nameof(OverlayBarVisibility));
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


        public MainViewModel(NavigationStore navigationStore)
        {
            OverlayBarVisibility = OverlayVisibility = Visibility.Collapsed;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
