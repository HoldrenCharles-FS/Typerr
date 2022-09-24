using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr.Commands
{
    public enum NavigationOption
    {
        GoToLibraryButton,
        GoToSubscriptionsButton,
        GoToSubscription,
        None
    }
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase _viewModel;
        private readonly MainViewModel _mainViewModel;
        private readonly NavigationOption _navigationOption;
        private readonly RssModel _rssModel;

        public NavigationCommand(NavigationStore navigationStore, ViewModelBase viewModel, MainViewModel mainViewModel, NavigationOption navigationOption, RssModel rssModel = null)
        {
            _navigationStore = navigationStore;
            _viewModel = viewModel;
            _mainViewModel = mainViewModel;
            _navigationOption = navigationOption;
            if (navigationOption == NavigationOption.GoToSubscription && rssModel != null)
            {
                _rssModel = rssModel;
            }
        }

        public override void Execute(object parameter)
        {
            if (_navigationOption == NavigationOption.GoToLibraryButton)
            {
                _mainViewModel.NavPanelViewModel.RadioHomeIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioLibraryIsChecked = true;
                _mainViewModel.NavPanelViewModel.RadioSubscriptionsIsChecked = false;
            }
            else if (_navigationOption == NavigationOption.GoToSubscriptionsButton)
            {
                _mainViewModel.NavPanelViewModel.RadioHomeIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioLibraryIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioSubscriptionsIsChecked = true;
            }
            else if (_navigationOption == NavigationOption.GoToSubscription)
            {
                _mainViewModel.NavPanelViewModel.RadioHomeIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioLibraryIsChecked = false;
                _mainViewModel.NavPanelViewModel.RadioSubscriptionsIsChecked = false;
                _mainViewModel.NavPanelViewModel.SetSubscriptionChecked(_rssModel);
            }
                _navigationStore.CurrentViewModel = _viewModel;
        }
    }
}
