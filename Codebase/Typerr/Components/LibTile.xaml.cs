using System.Windows;
using System.Windows.Controls;
using Typerr.Model;

namespace Typerr.Components
{
    /// <summary>
    /// Interaction logic for LibTile.xaml
    /// </summary>
    public partial class LibTile : UserControl
    {

        public LibTileModel Model
        {
            get { return (LibTileModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(LibTileModel), typeof(LibTile), new PropertyMetadata(new LibTileModel()));



        public string FooterInfo
        {
            get { return (string)GetValue(FooterInfoProperty); }
            set { SetValue(FooterInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FooterInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterInfoProperty =
            DependencyProperty.Register("FooterInfo", typeof(string), typeof(LibTile), new PropertyMetadata(string.Empty));




        public LibTile()
        {
            InitializeComponent();
        }
    }
}
