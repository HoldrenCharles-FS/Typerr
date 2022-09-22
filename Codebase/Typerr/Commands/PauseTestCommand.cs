using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class PauseTestCommand : CommandBase
    {
        private readonly TestPanelViewModel _testPanelViewModel;
        private readonly TestViewModel _testViewModel;

        public PauseTestCommand(TestPanelViewModel testPanelViewModel, TestViewModel testViewModel)
        {
            _testPanelViewModel = testPanelViewModel;
            _testViewModel = testViewModel;
        }

        public override void Execute(object parameter)
        {
            _testPanelViewModel.IsPaused = !_testPanelViewModel.IsPaused;
            
            if (_testPanelViewModel.IsPaused)
            {
                _testViewModel.Pause();
            }
            else
            {
                _testViewModel.Unpause();
            }
        }
    }
}
