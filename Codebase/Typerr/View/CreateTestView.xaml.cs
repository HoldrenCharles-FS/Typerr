using System.Windows;
using System.Windows.Controls;

namespace Typerr.View
{
    /// <summary>
    /// Interaction logic for CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : UserControl
    {
        public Grid Overlay { get; internal set; }
        public Grid OverlayBar { get; internal set; }

        public double CreateTestColumnWidth { get; internal set; }

        public CreateTestView()
        {
            InitializeComponent();
        }

        private void CreateTestClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Overlay.Visibility = Visibility.Hidden;
            OverlayBar.Visibility = Visibility.Hidden;

            TextAreaBox.Text = null;
        }
    }
}
