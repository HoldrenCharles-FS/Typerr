using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class ItemPreviewViewModel : ViewModelBase
    {
        public FeedTileViewModel FeedTileViewModel { get; }
        public ICommand DialogCloseCommand { get; }
        public ICommand GenerateTestCommand { get; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _pubDate;
        public string PubDate
        {
            get
            {
                return _pubDate;
            }
            set
            {
                _pubDate = value;
                OnPropertyChanged(nameof(PubDate));
            }
        }

        private double _descriptionHeight;
        public double DescriptionHeight
        {
            get
            {
                return _descriptionHeight;
            }
            set
            {
                _descriptionHeight = value;
                OnPropertyChanged(nameof(DescriptionHeight));
            }
        }

        public ItemPreviewViewModel(FeedTileViewModel feedTileViewModel, MainViewModel mainViewModel)
        {
            FeedTileViewModel = feedTileViewModel;
            DialogCloseCommand = new DialogCloseCommand(mainViewModel);
            GenerateTestCommand = new GenerateTestCommand(this, mainViewModel);
            Init();
        }

        private void Init()
        {
            Title = FeedTileViewModel.Title;
            Description = FeedTileViewModel.Description;
            DescriptionHeight = string.IsNullOrWhiteSpace(Description) ? 0 : 80;
            PubDate = (FeedTileViewModel.Item.Published.DateTime == DateTime.MinValue) ? "" : FeedTileViewModel.Item.Published.ToString("MMM dd yyyy");
        }
    }
}
