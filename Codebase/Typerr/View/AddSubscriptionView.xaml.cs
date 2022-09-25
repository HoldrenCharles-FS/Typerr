using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Typerr.ViewModel;

namespace Typerr.View
{
    /// <summary>
    /// Interaction logic for AddSubscriptionView.xaml
    /// </summary>
    public partial class AddSubscriptionView : UserControl
    {
        public AddSubscriptionView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RssBox.Text == AddSubscriptionViewModel.DefaultMessage)
            {
                RssBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RssBox.Text))
            {
                RssBox.Text = AddSubscriptionViewModel.DefaultMessage;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (RssBox.Text.Length > 0)
            {
                if (RssBox.Text == AddSubscriptionViewModel.DefaultMessage)
                {
                    RssBox.Text = "";
                }
            }
        }
    }
}
