using System.Windows;
using System.Windows.Controls;
using Typerr.Model;
using Typerr.View;
using Typerr.ViewModel;

namespace Typerr.Components
{
    /// <summary>
    /// Interaction logic for LibTile.xaml
    /// </summary>
    public partial class LibTile : UserControl
    {

        public LibTileViewModel VM
        {
            get { return (LibTileViewModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("ViewModel", typeof(LibTileViewModel), typeof(LibTile), new PropertyMetadata(new LibTileViewModel()));



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
            VM = new LibTileViewModel();
            FooterInfo = $"{VM.AuthorName} | {VM.WebsiteName}\n{VM.WordCount} words | {VM.TimeRemaining} remaining";
            InitializeComponent();
        }

        public LibTile(TestModel model, User user)
        {
            VM = new LibTileViewModel(model, user);
            FooterInfo = $"{VM.AuthorName} | {VM.WebsiteName}\n{VM.WordCount} words | {VM.TimeRemaining} remaining";
            InitializeComponent();
        }

    }
}
