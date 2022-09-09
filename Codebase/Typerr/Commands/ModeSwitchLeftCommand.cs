using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Service;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class ModeSwitchLeftCommand : CommandBase
    {
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public ModeSwitchLeftCommand(TestPreviewViewModel testPreviewViewModel)
        {
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_testPreviewViewModel.User.Mode == 0)
            {
                _testPreviewViewModel.User.Mode = Enum.GetNames(typeof(Mode)).Length - 1;
            }
            else
            {
                _testPreviewViewModel.User.Mode--;
            }

            _testPreviewViewModel.ModeText = TestService.GetMode(_testPreviewViewModel.User.Mode);
        }
    }
}
