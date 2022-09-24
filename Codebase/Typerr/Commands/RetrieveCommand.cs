using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.Service;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class RetrieveCommand : CommandBase
    {
        private readonly AddSubscriptionViewModel _addSubscriptionViewModel;
        private readonly MainViewModel _mainViewModel;

        public RetrieveCommand(AddSubscriptionViewModel addSubscriptionViewModel, MainViewModel mainViewModel)
        {
            _addSubscriptionViewModel = addSubscriptionViewModel;
            _mainViewModel = mainViewModel;
        }

        public override async void Execute(object parameter)
        {
            if (_mainViewModel.ContainsRssId(_addSubscriptionViewModel.RssField))
            {
                _addSubscriptionViewModel.SubscriptionExists = true;
                _addSubscriptionViewModel.RssField = AddSubscriptionViewModel.DefaultMessage;
                return;
            }
            RssModel rssModel = await RssService.Read(_addSubscriptionViewModel.RssField);

            if (rssModel.Uri == "-1")
            {
                _addSubscriptionViewModel.RssModel = null;
                return;
            }

            // Grabbing the retrieved value in case it gets edited in the field
            _addSubscriptionViewModel.RssValue = _addSubscriptionViewModel.RssField;

            _addSubscriptionViewModel.NameField = rssModel.Title;
            _addSubscriptionViewModel.NameFieldEnabled = true;
            _addSubscriptionViewModel.ButtonCommand = _addSubscriptionViewModel.SubscribeCommand;
            _addSubscriptionViewModel.ButtonText = "Subscribe";
            _addSubscriptionViewModel.NameLabelForeground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);

            _addSubscriptionViewModel.RssModel = rssModel;
        }
    }
}
