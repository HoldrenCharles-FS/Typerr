using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class DeleteNoCommand : CommandBase
    {
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public DeleteNoCommand(TestPreviewViewModel testPreviewViewModel)
        {
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            _testPreviewViewModel.DeleteTestControls.Clear();
            _testPreviewViewModel.DeleteTestControls.Add(_testPreviewViewModel.DeleteTextBlock);
        }
    }
}
