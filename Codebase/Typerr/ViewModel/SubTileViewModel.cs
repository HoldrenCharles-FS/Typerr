using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class SubTileViewModel : ViewModelBase
    {

        private readonly RssModel _rssModel;

        public ICommand SubTileCommand { get; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public SubTileViewModel(RssModel rssModel)
        {
            _rssModel = rssModel;
            SubTileCommand = new SubTileCommand(this);
        }
    }
}
