using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class AddSubscriptionViewModel : ViewModelBase
    {
        public ICommand DialogCloseCommand { get; }
        public AddSubscriptionViewModel(MainViewModel mainViewModel)
        {
            DialogCloseCommand = new DialogCloseCommand(mainViewModel);
        }
    }
}
