using System.Windows;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class DialogCloseCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public DialogCloseCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.OverlayVisibility = Visibility.Collapsed;
        }
    }
}
