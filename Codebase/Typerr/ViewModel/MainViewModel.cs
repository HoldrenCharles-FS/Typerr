using System.Windows;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

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
            OverlayVisibility = Visibility.Collapsed;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
