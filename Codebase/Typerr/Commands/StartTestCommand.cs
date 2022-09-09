using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Service;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class StartTestCommand : CommandBase
    {
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public StartTestCommand(TestPreviewViewModel testPreviewViewModel)
        {
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            _testPreviewViewModel.User.Minutes = _testPreviewViewModel.NumericUpDownValue;
            UserService.Write(_testPreviewViewModel.User);
        }
    }
}
