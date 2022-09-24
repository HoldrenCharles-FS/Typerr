using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.Service;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public class SubscribeCommand : CommandBase
    {
        private readonly AddSubscriptionViewModel _addSubscriptionViewModel;
        private readonly MainViewModel _mainViewModel;

        public SubscribeCommand(AddSubscriptionViewModel addSubscriptionViewModel, MainViewModel mainViewModel)
        {
            _addSubscriptionViewModel = addSubscriptionViewModel;
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_addSubscriptionViewModel.RssModel != null)
            {
                Subscription subscription;
                subscription.name = _addSubscriptionViewModel.RssModel.Title = (string.IsNullOrWhiteSpace(_addSubscriptionViewModel.NameField))
                        ? _addSubscriptionViewModel.RssModel.Title
                        : _addSubscriptionViewModel.NameField;
                subscription.url = _addSubscriptionViewModel.RssValue;
                _mainViewModel.User.Subscriptions.Add(subscription);
                UserService.Write(_mainViewModel.User);

                _mainViewModel.AddSubTile(_addSubscriptionViewModel.RssModel);
                _mainViewModel.HomeViewModel.RefreshSubscriptions();
                _addSubscriptionViewModel.DialogCloseCommand.Execute(null);
            }
            else
            {
                // TODO: Validation Rss Failed to Retrieve?
            }
        }
    }
}
