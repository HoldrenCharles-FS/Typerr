using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class CreateTestCloseCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public CreateTestCloseCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.OverlayBarVisibility = System.Windows.Visibility.Collapsed;
            _mainViewModel.OverlayVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
