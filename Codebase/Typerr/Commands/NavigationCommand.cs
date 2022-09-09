using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _viewModel;

        public NavigationCommand(NavigationStore navigationStore, ViewModelBase viewModel)
        {
            _navigationStore = navigationStore;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _viewModel;
        }
    }
}
