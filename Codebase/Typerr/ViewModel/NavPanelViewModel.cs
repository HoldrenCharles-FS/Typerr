using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Typerr.ViewModel
{
    public class NavPanelViewModel : ViewModelBase
    {
        public ICommand GoToHomeCommand { get; }
        public ICommand GoToLibraryCommand { get; }
        public NavPanelViewModel(ICommand goToHomeCommand, ICommand goToLibraryCommand)
        {
            GoToHomeCommand = goToHomeCommand;
            GoToLibraryCommand = goToLibraryCommand;
        }
    }
}
