using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class CreateTestCloseCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public CreateTestCloseCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.OverlayVisibility = Visibility.Collapsed;
        }
    }
}
