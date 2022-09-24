using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class FeedTileViewModel : ViewModelBase
    {
        public ISyndicationItem Item { get; }
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

        private string _source;
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        public FeedTileViewModel(ISyndicationItem syndicationItem, string source)
        {
            Item = syndicationItem;
            Title = syndicationItem.Title;
            Description = syndicationItem.Description;
            Source = source;
           // PubDate = syndicationItem.Published.ToString("MMMM dd yyyy");
        }
    }
}
