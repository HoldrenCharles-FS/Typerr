using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    class NavigationCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigationCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            //_navigationStore.CurrentViewModel = new HomeViewModel();
        }
    }
}
