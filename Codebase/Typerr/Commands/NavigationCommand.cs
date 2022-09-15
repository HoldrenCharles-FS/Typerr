using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public enum NavigationOption
    {
        GoToLibraryButton,
        None
    }
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _viewModel;
        private readonly MainViewModel _mainViewModel;
        private readonly NavigationOption _navigationOption;

        public NavigationCommand(NavigationStore navigationStore, ViewModelBase viewModel, MainViewModel mainViewModel, NavigationOption navigationOption)
        {
            _navigationStore = navigationStore;
            _viewModel = viewModel;
            _mainViewModel = mainViewModel;
            _navigationOption = navigationOption;
        }

        public override void Execute(object parameter)
        {
            if (_navigationOption == NavigationOption.GoToLibraryButton)
            {
                _mainViewModel.NavPanelViewModel.RadioHomeIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioLibraryIsChecked = true;
            }
            _navigationStore.CurrentViewModel = _viewModel;
        }
    }
}
