using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class SubMenuButtonViewModel : ViewModelBase
    {
        public RssModel RssModel { get; }

        public ICommand NavigationCommand { get; }

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

        private bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public SubMenuButtonViewModel(NavigationStore navigationStore, MainViewModel mainViewModel, RssModel rssModel)
        {
            RssModel = rssModel;
            NavigationCommand = new NavigationCommand(navigationStore, new SubscriptionViewModel(rssModel, mainViewModel), mainViewModel, NavigationOption.None);
            Init();
        }

        private void Init()
        {
            Name = RssModel.Title;
            IsChecked = false;
        }
    }
}
