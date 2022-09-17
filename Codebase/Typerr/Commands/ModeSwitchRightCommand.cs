using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Typerr.Service;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class ModeSwitchRightCommand : CommandBase
    {
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public ModeSwitchRightCommand(TestPreviewViewModel testPreviewViewModel)
        {
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_testPreviewViewModel.User.Mode == Enum.GetNames(typeof(Mode)).Length - 1)
            {
                _testPreviewViewModel.User.Mode = 0;
            }
            else
            {
                _testPreviewViewModel.User.Mode++;
            }

            _testPreviewViewModel.ModeText = FormatService.GetMode(_testPreviewViewModel.User.Mode);
            _testPreviewViewModel.NumericUpDownWidth = (_testPreviewViewModel.User.Mode == 0)
                ? 60 : 0;
        }
    }
}
