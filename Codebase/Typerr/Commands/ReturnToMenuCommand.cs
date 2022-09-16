using System;
using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class ReturnToMenuCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public ReturnToMenuCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.OverlayVisibility = Visibility.Collapsed;
            _mainViewModel.SetCurrentView(_mainViewModel.HomeViewModel);
            _mainViewModel.CurrentPanel = _mainViewModel.NavPanelViewModel;
        }
    }
}
