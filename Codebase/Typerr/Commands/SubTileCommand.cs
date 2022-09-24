using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class SubTileCommand : CommandBase
    {
        private readonly SubTileViewModel _subTileViewModel;

        public SubTileCommand(SubTileViewModel subTileViewModel)
        {
            _subTileViewModel = subTileViewModel;
        }

        public override void Execute(object parameter)
        {
            // TODO: Navigate to Subscription page
        }
    }
}
