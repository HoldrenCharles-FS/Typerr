using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class AddSubscriptionViewModel : ViewModelBase
    {
        public RssModel RssModel { get; set; }
        public ICommand DialogCloseCommand { get; }
        public ICommand RetrieveCommand { get; }
        public ICommand SubscribeCommand { get; }

        private ICommand _buttonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return _buttonCommand;
            }
            set
            {
                _buttonCommand = value;
                OnPropertyChanged(nameof(ButtonCommand));
            }
        }

        private string _rssField;
        public string RssField
        {
            get
            {
                return _rssField;
            }
            set
            {
                ValidateRss(value);
                _rssField = value;
                OnPropertyChanged(nameof(RssField));
            }
        }
        public string RssValue { get; set; }

        private string _nameField;
        public string NameField
        {
            get
            {
                return _nameField;
            }
            set
            {
                _nameField = value;
                OnPropertyChanged(nameof(NameField));
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get
            {
                return _buttonText;
            }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private bool _nameFieldEnabled;
        public bool NameFieldEnabled
        {
            get
            {
                return _nameFieldEnabled;
            }
            set
            {
                _nameFieldEnabled = value;
                OnPropertyChanged(nameof(NameFieldEnabled));
            }
        }

        private SolidColorBrush _nameLabelForeground;
        public SolidColorBrush NameLabelForeground
        {
            get
            {
                return _nameLabelForeground;
            }
            set
            {
                _nameLabelForeground = value;
                OnPropertyChanged(nameof(NameLabelForeground));
            }
        }

        private SolidColorBrush _disabledBrush = new SolidColorBrush(Color.FromArgb(255, 136, 136, 136));

        public static string DefaultMessage { get; } = "Enter an RSS Feed URL Here...";
        public AddSubscriptionViewModel(MainViewModel mainViewModel)
        {
            DialogCloseCommand = new DialogCloseCommand(mainViewModel);
            SubscribeCommand = new SubscribeCommand(this, mainViewModel);
            RetrieveCommand = new RetrieveCommand(this);
            Init();
        }

        private void Init()
        {
            RssField = DefaultMessage;
            ResetState();
        }

        private void ResetState()
        {
            NameField = "";
            NameFieldEnabled = false;
            NameLabelForeground = _disabledBrush;
            ButtonText = "Retrieve";
            ButtonCommand = RetrieveCommand;
        }

        private void ValidateRss(string value)
        {
            if ((Uri.IsWellFormedUriString(_rssField, UriKind.Absolute)
                && !Uri.IsWellFormedUriString(value, UriKind.Absolute))
                || (Uri.IsWellFormedUriString(_rssField, UriKind.Absolute)
                && Uri.IsWellFormedUriString(value, UriKind.Absolute)
                && value != _rssField))
            {
                ResetState();
            }
        }
    }
}
