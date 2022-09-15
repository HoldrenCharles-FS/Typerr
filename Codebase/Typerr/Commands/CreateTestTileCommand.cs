using System.Windows;
using Typerr.Stores;
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
            _mainViewModel.CurrentDialog = _mainViewModel.CreateTestViewModel;
            _mainViewModel.OverlayVisibility = Visibility.Visible;
            
        }
    }
}
