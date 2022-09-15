using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class DeleteYesCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly TestPreviewViewModel _testPreviewViewModel;

        public DeleteYesCommand(TestPreviewViewModel testPreviewViewModel, HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
            _testPreviewViewModel = testPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                FileSystem.DeleteFile(_testPreviewViewModel.TestModel.FileName.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                _homeViewModel.MainViewModel.RemoveLibTile(_testPreviewViewModel.TestModel.FileName);
                _homeViewModel.RefreshLibrary();
                _testPreviewViewModel.TestPreviewCloseCommand.Execute(parameter);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
