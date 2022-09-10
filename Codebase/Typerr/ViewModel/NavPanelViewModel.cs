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
        public NavPanelViewModel(ICommand goToHomeCommand, ICommand goToLibraryCommand)
        {
            GoToHomeCommand = goToHomeCommand;
            GoToLibraryCommand = goToLibraryCommand;
            RadioHomeIsChecked = true;
            RadioLibraryIsChecked = false;
        }
    }
}
