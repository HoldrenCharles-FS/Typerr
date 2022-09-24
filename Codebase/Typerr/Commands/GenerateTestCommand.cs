using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.ViewModel;
using TyperrDemo.Services;

namespace Typerr.Commands
{
    public class GenerateTestCommand : CommandBase
    {
        private readonly ItemPreviewViewModel _itemPreviewViewModel;
        private readonly MainViewModel _mainViewModel;

        public GenerateTestCommand(ItemPreviewViewModel itemPreviewViewModel, MainViewModel mainViewModel)
        {
            _itemPreviewViewModel = itemPreviewViewModel;
            _mainViewModel = mainViewModel;
        }

        public override async void Execute(object parameter)
        {
            _mainViewModel.CreateTestViewModel.TextArea = _itemPreviewViewModel.FeedTileViewModel.Item.Id;
            _mainViewModel.CreateTestViewModel.GetTestCommand.Execute(null);
            _mainViewModel.CurrentDialog = _mainViewModel.CreateTestViewModel;
            _mainViewModel.OverlayVisibility = System.Windows.Visibility.Visible;
        }
    }
}
