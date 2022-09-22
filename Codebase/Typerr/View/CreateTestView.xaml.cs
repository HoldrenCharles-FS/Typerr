using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Typerr.Service;
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
