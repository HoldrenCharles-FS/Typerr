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
        private readonly MainViewModel _mainViewModel;

        public IEnumerable<SubTileViewModel> SubTileViewModels => _mainViewModel.AllSubTileViewModels;
        public IEnumerable<FeedTileViewModel> FeedTileViewModels => _mainViewModel.AllFeedTileViewModels;

        public SubscriptionsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            AddSubscriptionTileCommand = new AddSubscriptionTileCommand(mainViewModel);
        }
    }
}
