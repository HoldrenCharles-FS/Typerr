using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class AddSubscriptionViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly MainViewModel _mainViewModel;
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

        private bool _subscriptionExists;
        public bool SubscriptionExists
        {
            get
            {
                return _subscriptionExists;
            }
            set
            {
                _subscriptionExists = value;
                OnPropertyChanged(nameof(SubscriptionExists));
                if (_subscriptionExists)
                {
                    ValidateRss(_rssField);
                }
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

        private SolidColorBrush _rssFieldBrush;
        public SolidColorBrush RssFieldBrush
        {
            get
            {
                return _rssFieldBrush;
            }
            set
            {
                _rssFieldBrush = value;
                OnPropertyChanged(nameof(RssFieldBrush));
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

        private readonly Dictionary<string, List<string>> _propertyErrors;

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private SolidColorBrush _disabledBrush = new SolidColorBrush(Color.FromArgb(255, 136, 136, 136));

        public static string DefaultMessage { get; } = "Enter an RSS Feed URL Here...";
        public AddSubscriptionViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            DialogCloseCommand = new DialogCloseCommand(mainViewModel);
            SubscribeCommand = new SubscribeCommand(this, mainViewModel);
            RetrieveCommand = new RetrieveCommand(this, mainViewModel);
            _propertyErrors = new Dictionary<string, List<string>>();
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
            SubscriptionExists = false;
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
            else
            {
                ClearError(nameof(RssField));
                if (SubscriptionExists)
                {
                    SubscriptionExists = false;
                    RssFieldBrush = new SolidColorBrush(Colors.Red);
                    UpdateError(nameof(RssField), $"You already subscribed to this feed as {_mainViewModel.FindSubscriptionName(RssField)}");
                }
                else
                {
                    RssFieldBrush = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
                }
            }


        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }

        public void UpdateError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            if (_propertyErrors.Values.Any(iList => iList.Count() == 0))
            {
                _propertyErrors[propertyName].Add(errorMessage);
            }
            else
            {
                _propertyErrors[propertyName][0] = errorMessage;
            }
            OnErrorChanged(propertyName);
        }

        private void ClearError(string propertyName)
        {
            if (_propertyErrors.Count > 0)
            {
                _propertyErrors[propertyName][0] = "";
                OnErrorChanged(propertyName);
            }
        }

        private void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
