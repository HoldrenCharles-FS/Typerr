using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class PauseTestCommand : CommandBase
    {
        private readonly TestPanelViewModel _testPanelViewModel;

        public PauseTestCommand(TestPanelViewModel testPanelViewModel)
        {
            _testPanelViewModel = testPanelViewModel;
        }

        public override void Execute(object parameter)
        {
            Console.WriteLine();
        }
    }
}
