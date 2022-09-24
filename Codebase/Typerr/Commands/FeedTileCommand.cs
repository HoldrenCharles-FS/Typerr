using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class FeedTileCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly FeedTileViewModel _feedTileViewModel;

        public FeedTileCommand(FeedTileViewModel feedTileViewModel, MainViewModel mainViewModel)
        {
            _feedTileViewModel = feedTileViewModel;
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.CurrentDialog = new ItemPreviewViewModel(_feedTileViewModel, _mainViewModel);
            _mainViewModel.OverlayVisibility = System.Windows.Visibility.Visible;
        }
    }
}
