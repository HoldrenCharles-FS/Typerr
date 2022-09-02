using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class CreateTestTileCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public CreateTestTileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.OverlayBarVisibility = Visibility.Visible;
            _mainViewModel.OverlayVisibility = Visibility.Visible;
        }
    }
}
