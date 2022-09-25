using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class OpenLinkCommand : CommandBase
    {
        private readonly ItemPreviewViewModel _itemPreviewViewModel;

        public OpenLinkCommand(ItemPreviewViewModel itemPreviewViewModel)
        {
            _itemPreviewViewModel = itemPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            System.Diagnostics.Process.Start("explorer", _itemPreviewViewModel.URL);
        }
    }
}
