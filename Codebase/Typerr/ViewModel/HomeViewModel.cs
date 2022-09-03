using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ObservableCollection<LibTileViewModel> _libTiles;

        public IEnumerable<LibTileViewModel> LibTiles => _libTiles;

        public ICommand CreateTestTileCommand { get; }

        public HomeViewModel(ICommand command)
        {
            CreateTestTileCommand = command;
            _libTiles = new ObservableCollection<LibTileViewModel>();
        }
    }
}