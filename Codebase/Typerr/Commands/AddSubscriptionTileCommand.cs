using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class AddSubscriptionTileCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        public AddSubscriptionTileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.CurrentDialog = new AddSubscriptionViewModel(_mainViewModel);
            _mainViewModel.OverlayVisibility = Visibility.Visible;
        }
    }
}
