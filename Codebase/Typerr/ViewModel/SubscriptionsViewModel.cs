using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class SubscriptionsViewModel : ViewModelBase
    {
        public ICommand AddSubscriptionTileCommand { get; }
        public ICommand ManageSubscriptionsCommand { get; }
        private readonly MainViewModel _mainViewModel;

        public IEnumerable<SubTileViewModel> SubTileViewModels => _mainViewModel.AllSubTileViewModels;
        public IEnumerable<FeedTileViewModel> FeedTileViewModels => _mainViewModel.AllFeedTileViewModels;

        public SubscriptionsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            AddSubscriptionTileCommand = new AddSubscriptionTileCommand(mainViewModel);
           // ManageSubscriptionsCommand = new ManageSubscriptionsCommand(this);
        }

        public void ManageSubscriptions()
        {
            foreach (FeedTileViewModel feed in FeedTileViewModels)
            {
                feed.ButtonIsHitTestVisible = false;
            }
            foreach (SubTileViewModel sub in SubTileViewModels)
            {
                sub.DeleteButtonVisibility = System.Windows.Visibility.Visible;
                sub.IsHitTestVisible = false;
            }
        }
    }
}
