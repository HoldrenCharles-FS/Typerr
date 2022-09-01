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

namespace Typerr.View
{
    /// <summary>
    /// Interaction logic for CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : UserControl
    {
        public CreateTestView()
        {
            TextAreaBox = new TextBox();
            TextAreaBox.Height = App.Current.MainWindow.Height - 200;
            TextAreaBox.Width = App.Current.MainWindow.Width - 400;
            OpenFromFileButton.Width = TextAreaBox.Width / 3;
            OpenFromFileButton.Height = TextAreaBox.Height / 4;
            InitializeComponent();
        }
    }
}
