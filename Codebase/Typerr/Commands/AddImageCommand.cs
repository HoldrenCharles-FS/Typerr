using Microsoft.Win32;
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
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlg.Title = "Add a cover image";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                _createTestViewModel.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(dlg.FileName));
            }
        }
    }
}
