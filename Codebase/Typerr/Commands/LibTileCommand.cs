using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Typerr.Model;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class LibTileCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        private readonly TestPreviewViewModel _testPreviewViewModel;

        public LibTileCommand(MainViewModel mainViewModel, TestPreviewViewModel testPreviewViewModel)
        {
            _mainViewModel = mainViewModel;
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            _mainViewModel.CurrentDialog = _testPreviewViewModel;
            _mainViewModel.OverlayVisibility = Visibility.Visible;
        }
    }
}
