using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Typerr.Model;
using Typerr.Stores;

namespace Typerr.ViewModel
{
    public class NavPanelViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ICommand GoToHomeCommand { get; }
        public ICommand GoToLibraryCommand { get; }
        public ICommand GoToSubscriptionsCommand { get; }

        private ObservableCollection<SubMenuButtonViewModel> _subMenuButtonViewModels;

        public ObservableCollection<SubMenuButtonViewModel> SubMenuButtonViewModels
        {
            get { return _subMenuButtonViewModels; }
            set { _subMenuButtonViewModels = value; }
        }

        private bool _radioHomeIsChecked;
        public bool RadioHomeIsChecked
        {
            get
            {
                return _radioHomeIsChecked;
            }
            set
            {
                if (value)
                {
                    RadioLibraryIsChecked = false;
                }
                _radioHomeIsChecked = value;
                OnPropertyChanged(nameof(RadioHomeIsChecked));
            }
        }

        private bool _radioLibraryIsChecked;
        public bool RadioLibraryIsChecked
        {
            get
            {
                return _radioLibraryIsChecked;
            }
            set
            {
                if (value)
                {
                    RadioHomeIsChecked = false;
                }
                _radioLibraryIsChecked = value;
                OnPropertyChanged(nameof(RadioLibraryIsChecked));
            }
        }

        private bool _radioSubscriptionsIsChecked;
        public bool RadioSubscriptionsIsChecked
        {
            get
            {
                return _radioSubscriptionsIsChecked;
            }
            set
            {
                _radioSubscriptionsIsChecked = value;
                OnPropertyChanged(nameof(RadioSubscriptionsIsChecked));
            }
        }


        public NavPanelViewModel(NavigationStore navigationStore, ICommand goToHomeCommand, ICommand goToLibraryCommand, ICommand goToSubscriptionsCommand)
        {
            _navigationStore = navigationStore;
            GoToHomeCommand = goToHomeCommand;
            GoToLibraryCommand = goToLibraryCommand;
            GoToSubscriptionsCommand = goToSubscriptionsCommand;
            RadioHomeIsChecked = true;
            RadioLibraryIsChecked = false;
            RadioSubscriptionsIsChecked = false;
            _subMenuButtonViewModels = new ObservableCollection<SubMenuButtonViewModel>();
        }

        public void AddSubButton(RssModel rssModel, MainViewModel mainViewModel)
        {
            _subMenuButtonViewModels.Insert(0, new SubMenuButtonViewModel(_navigationStore, mainViewModel, rssModel));
        }

        public void RemoveSubButton(string uri)
        {
            foreach (SubMenuButtonViewModel subButton in _subMenuButtonViewModels)
            {
                if (subButton.RssModel.Uri == uri)
                {
                    _subMenuButtonViewModels.Remove(subButton);
                    break;
                }
            }
        }

        public void SetSubscriptionChecked(RssModel rssModel)
        {
            foreach (SubMenuButtonViewModel button in SubMenuButtonViewModels)
            {
                button.IsChecked = false;
                if (button.RssModel.Uri == rssModel.Uri)
                {
                    button.IsChecked = true;
                }
            }
        }
    }
}
