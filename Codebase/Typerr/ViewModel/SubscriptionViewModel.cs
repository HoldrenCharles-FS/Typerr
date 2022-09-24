using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class SubscriptionViewModel : ViewModelBase
    {
        public RssModel RssModel { get; }

        private ObservableCollection<FeedTileViewModel> _feedTiles;

        public ObservableCollection<FeedTileViewModel> FeedTiles
        {
            get { return _feedTiles; }
            set { _feedTiles = value; }
        }

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

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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

        public SubscriptionViewModel(RssModel rssModel, MainViewModel mainViewModel)
        {
            RssModel = rssModel;
            _feedTiles = new ObservableCollection<FeedTileViewModel>();
            Init(mainViewModel);
        }

        private void Init(MainViewModel mainViewModel)
        {
            Name = RssModel.Title;
            Description = RssModel.Description;
            Image = RssModel.Image;

            foreach (ISyndicationItem item in RssModel.Items)
            {
                _feedTiles.Insert(0, new FeedTileViewModel(item, RssModel.Title, mainViewModel));
            }
        }
    }
}
