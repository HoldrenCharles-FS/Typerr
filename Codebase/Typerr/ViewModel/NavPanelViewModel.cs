using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Typerr.ViewModel
{
    public class NavPanelViewModel : ViewModelBase
    {
        public ICommand GoToHomeCommand { get; }
        public ICommand GoToLibraryCommand { get; }
        public ICommand GoToSubscriptionsCommand { get; }

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
        public NavPanelViewModel(ICommand goToHomeCommand, ICommand goToLibraryCommand, ICommand goToSubscriptionsCommand)
        {
            GoToHomeCommand = goToHomeCommand;
            GoToLibraryCommand = goToLibraryCommand;
            GoToSubscriptionsCommand = goToSubscriptionsCommand;
            RadioHomeIsChecked = true;
            RadioLibraryIsChecked = false;
            RadioSubscriptionsIsChecked = false;
        }
    }
}
