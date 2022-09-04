using System;
using System.Windows;
using System.Windows.Controls;
using Typerr.ViewModel;

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

        private void TextAreaBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextAreaBox.Text == CreateTestViewModel.DefaultMessage)
            {
                TextAreaBox.Text = "";
            }
        }

        private void TextAreaBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextAreaBox.Text))
            {
                TextAreaBox.Text = CreateTestViewModel.DefaultMessage;
            }
        }
    }
}
