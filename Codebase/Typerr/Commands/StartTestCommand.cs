using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Service;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class StartTestCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public StartTestCommand(NavigationStore navigationStore, TestPreviewViewModel testPreviewViewModel)
        {
            _navigationStore = navigationStore;
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            _testPreviewViewModel.User.Minutes = _testPreviewViewModel.NumericUpDownValue;
            UserService.Write(_testPreviewViewModel.User);

            _testPreviewViewModel.TestPreviewCloseCommand.Execute(parameter);

            _navigationStore.CurrentViewModel = new TestViewModel(_testPreviewViewModel.TestModel);
        }
    }
}
