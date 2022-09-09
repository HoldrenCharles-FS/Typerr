using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class LibraryViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; }

        private ObservableCollection<LibTileViewModel> _libTiles;
        public ObservableCollection<LibTileViewModel> LibTiles
        {
            get
            {
                return _libTiles;
            }
            set
            {
                _libTiles = value;
                OnPropertyChanged(nameof(LibTiles));
            }
        }

        public LibraryViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            Init();
        }

        private void Init()
        {
            LibTiles = new ObservableCollection<LibTileViewModel>();
            LibTiles = (ObservableCollection<LibTileViewModel>)MainViewModel.AllLibTileViewModels;
        }
    }
}
