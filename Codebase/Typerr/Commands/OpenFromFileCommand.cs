using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class OpenFromFileCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;

        public OpenFromFileCommand(CreateTestViewModel createTestViewModel)
        {
            _createTestViewModel = createTestViewModel;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlg.Title = "Import from text file...";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string data = File.ReadAllText(dlg.FileName);

                data = data.TrimEnd('\r', '\n');

                _createTestViewModel.TextArea = data;
            }
        }
    }
}
