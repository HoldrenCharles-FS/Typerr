using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class RemoveImageCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;

        public RemoveImageCommand(CreateTestViewModel createTestViewModel)
        {
            _createTestViewModel = createTestViewModel;
        }

        public override void Execute(object parameter)
        {
            _createTestViewModel.Image = null;
            _createTestViewModel.UploadImagePrompt = _createTestViewModel.UploadImagePrompt;
        }
    }
}
