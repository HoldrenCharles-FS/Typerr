using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class DeleteTestCommand : CommandBase
    {
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public DeleteTestCommand(TestPreviewViewModel testPreviewViewModel)
        {
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            _testPreviewViewModel.DeleteTestControls.Clear();
            _testPreviewViewModel.DeleteTestControls.Add(_testPreviewViewModel.YesNoTextBlock);
        }
    }
}
