using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class SubTileViewModel : ViewModelBase
    {

        public RssModel RssModel { get; }

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

        public SubTileViewModel(RssModel rssModel, NavigationStore navigationStore, MainViewModel mainViewModel)
        {
            RssModel = rssModel;
            SubTileCommand = new NavigationCommand(navigationStore, new SubscriptionViewModel(rssModel, mainViewModel), mainViewModel, NavigationOption.GoToSubscription, rssModel);
            Init();
        }

        private void Init()
        {
            Name = RssModel.Title;
            Image = RssModel.Image;
        }
    }
}
