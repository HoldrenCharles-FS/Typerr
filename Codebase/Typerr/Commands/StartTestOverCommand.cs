using System;
using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class StartTestOverCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly TestViewModel _testViewModel;

        public StartTestOverCommand(MainViewModel mainViewModel, TestViewModel testViewModel)
        {
            _mainViewModel = mainViewModel;
            _testViewModel = testViewModel;
        }

        public override void Execute(object parameter)
        {
            TestViewModel testViewModel = new TestViewModel(_testViewModel.TestModel, _mainViewModel.User);
            _mainViewModel.OverlayVisibility = Visibility.Collapsed;
            TestPanelViewModel testPanelViewModel = new TestPanelViewModel(_testViewModel, _mainViewModel.User, _testViewModel.TestModel.WordCount, _mainViewModel);
            testViewModel.TestPanelVM = testPanelViewModel;
            testPanelViewModel.TestVM = testViewModel;
            _mainViewModel.CurrentPanel = testPanelViewModel;
            _testViewModel.TestPanelVM = testPanelViewModel;

            _mainViewModel.SetCurrentView(testViewModel);
        }
    }
}
