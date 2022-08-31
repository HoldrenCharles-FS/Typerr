using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ObservableCollection<LibTileViewModel> _libTiles;

        public IEnumerable<LibTileViewModel> LibTiles => _libTiles;

        public ICommand CreateTestTileCommand { get; }

        public HomeViewModel()
        {
            _libTiles = new ObservableCollection<LibTileViewModel>();
            _libTiles.Add(new LibTileViewModel(new Model.TestModel(new Model.Article("Article title", "text", "summary", "Author name", "website.com", ""), new Model.TestData()), new Model.User(33)));
        }
    }
}