using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Typerr.ViewModel
{
    public class CreateTestTileViewModel : ViewModelBase
    {
        public ICommand CreateTestTileCommand { get; }

        public CreateTestTileViewModel(ICommand command)
        {
            CreateTestTileCommand = command;
        }

    }
}
