using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
        public ICommand RemoveSubscriptionCommand { get; }

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

        private Visibility _deleteButtonVisibility;
        public Visibility DeleteButtonVisibility
        {
            get
            {
                return _deleteButtonVisibility;
            }
            set
            {
                _deleteButtonVisibility = value;
                OnPropertyChanged(nameof(DeleteButtonVisibility));
            }
        }

        private bool _buttonIsHitTestVisible;
        public bool IsHitTestVisible
        {
            get
            {
                return _buttonIsHitTestVisible;
            }
            set
            {
                _buttonIsHitTestVisible = value;
                OnPropertyChanged(nameof(IsHitTestVisible));
            }
        }

        public SubTileViewModel(RssModel rssModel, NavigationStore navigationStore, MainViewModel mainViewModel)
        {
            RssModel = rssModel;
            SubTileCommand = new NavigationCommand(navigationStore, new SubscriptionViewModel(rssModel, mainViewModel), mainViewModel, NavigationOption.GoToSubscription, rssModel);
          //  RemoveSubscriptionCommand = new RemoveSubscriptionCommand(this, mainViewModel);
            Init();
        }

        private void Init()
        {
            Name = RssModel.Title;
            Image = RssModel.Image;
            DeleteButtonVisibility = Visibility.Hidden;
            IsHitTestVisible = true;
        }
    }
}
