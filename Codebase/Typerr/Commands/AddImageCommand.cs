using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class AddImageCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;

        public AddImageCommand(CreateTestViewModel createTestViewModel)
        {
            _createTestViewModel = createTestViewModel;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
