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

namespace Typerr.Components
{
    /// <summary>
    /// Interaction logic for CreateTestTile.xaml
    /// </summary>
    public partial class CreateTestTile : UserControl
    {


        public CreateTestTileViewModel ViewModel
        {
            get { return (CreateTestTileViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(CreateTestTileViewModel), typeof(CreateTestTile), new PropertyMetadata(new CreateTestTileViewModel(null)));



        public CreateTestTile()
        {
            InitializeComponent();
        }

        private void CreateTestButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
