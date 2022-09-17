using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public enum StartTestOption
    {
        Start,
        Resume
    }
    public class StartTestCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly TestPreviewViewModel _testPreviewViewModel;
        private readonly MainViewModel _mainViewModel;
        private readonly User _user;
        private readonly StartTestOption _startTestOption;

        public StartTestCommand(NavigationStore navigationStore, 
            TestPreviewViewModel testPreviewViewModel, MainViewModel mainViewModel, StartTestOption startTestOption)
        {
            _navigationStore = navigationStore;
            _testPreviewViewModel = testPreviewViewModel;
            _mainViewModel = mainViewModel;
            _user = _testPreviewViewModel.User;
            _startTestOption = startTestOption;
        }

        public override void Execute(object parameter)
        {
            if (_startTestOption == StartTestOption.Start)
            {
                _testPreviewViewModel.TestModel.testData.Reset();
            }

            _testPreviewViewModel.User.Minutes = _testPreviewViewModel.NumericUpDownValue;
            UserService.Write(_testPreviewViewModel.User);

            _testPreviewViewModel.TestPreviewCloseCommand.Execute(parameter);
            TestViewModel testViewModel = new TestViewModel(_testPreviewViewModel.TestModel, _user);
            _navigationStore.CurrentViewModel = testViewModel;
            TestPanelViewModel testPanelViewModel = new TestPanelViewModel(testViewModel, _user, _testPreviewViewModel.TestModel.WordCount, _mainViewModel);
            _mainViewModel.CurrentPanel = testPanelViewModel;
            testViewModel.TestPanelVM = testPanelViewModel;
            
        }
    }
}
