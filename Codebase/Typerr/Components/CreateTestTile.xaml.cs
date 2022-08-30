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

namespace Typerr.Components
{
    /// <summary>
    /// Interaction logic for CreateTestTile.xaml
    /// </summary>
    public partial class CreateTestTile : UserControl
    {
        public CreateTestTile()
        {
            InitializeComponent();
        }

        private void CreateTestButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("test");
        }
    }
}
