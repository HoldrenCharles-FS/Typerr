using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class StopTestCommand : CommandBase
    {
        private readonly TestPanelViewModel _testPanelViewModel;
        private readonly MainViewModel _mainViewModel;

        public StopTestCommand(TestPanelViewModel testPanelViewModel, MainViewModel mainViewModel)
        {
            _testPanelViewModel = testPanelViewModel;
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.CurrentDialog = new ResultsViewModel();
            _mainViewModel.OverlayVisibility = Visibility.Visible;
        }

        
    }
}
