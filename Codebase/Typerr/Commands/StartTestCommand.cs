using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class StartTestCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly TestPreviewViewModel _testPreviewViewModel;
        private readonly MainViewModel _mainViewModel;
        private readonly User _user;

        public StartTestCommand(NavigationStore navigationStore, TestPreviewViewModel testPreviewViewModel, MainViewModel mainViewModel, User user)
        {
            _navigationStore = navigationStore;
            _testPreviewViewModel = testPreviewViewModel;
            _mainViewModel = mainViewModel;
            _user = user;
        }

        public override void Execute(object parameter)
        {
            _testPreviewViewModel.User.Minutes = _testPreviewViewModel.NumericUpDownValue;
            UserService.Write(_testPreviewViewModel.User);

            _testPreviewViewModel.TestPreviewCloseCommand.Execute(parameter);
            TestViewModel testViewModel = new TestViewModel(_testPreviewViewModel.TestModel);
            _navigationStore.CurrentViewModel = testViewModel;
            _mainViewModel.CurrentPanel = new TestPanelViewModel(testViewModel, _user, _testPreviewViewModel.TestModel.WordCount, _mainViewModel);
            
        }
    }
}
