using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class FeedTileViewModel : ViewModelBase
    {
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

        public FeedTileViewModel(ISyndicationItem syndicationItem)
        {
            Title = syndicationItem.Title;
            Description = syndicationItem.Description;
            Source = syndicationItem.Id;
            PubDate = syndicationItem.Published.ToString("MMMM dd yyyy");
        }
    }
}
