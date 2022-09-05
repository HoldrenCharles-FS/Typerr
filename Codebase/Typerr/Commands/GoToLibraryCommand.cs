using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class GoToLibraryCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public GoToLibraryCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new LibraryViewModel();
        }
    }
}
