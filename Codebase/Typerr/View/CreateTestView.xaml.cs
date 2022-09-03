using System;
using System.Windows;
using System.Windows.Controls;

namespace Typerr.View
{
    /// <summary>
    /// Interaction logic for CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : UserControl
    {
        public CreateTestView()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(TextAreaBox, OnDescriptionPaste);
        }
        private void OnDescriptionPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true))
                return;

            var pastedText = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            if (string.IsNullOrEmpty(pastedText))
                return;

            if (Uri.IsWellFormedUriString(pastedText, UriKind.Absolute))
            {
                TextAreaBox.Text = pastedText;
                GetTestButton.Command.Execute(e.DataObject);
            }
        }
    }
}
