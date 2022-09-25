using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class FeedTileViewModel : ViewModelBase
    {
        public ISyndicationItem Item { get; }

        public ICommand FeedTileCommand { get; }

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

        private bool _buttonIsHitTestVisible;
        public bool ButtonIsHitTestVisible
        {
            get
            {
                return _buttonIsHitTestVisible;
            }
            set
            {
                _buttonIsHitTestVisible = value;
                OnPropertyChanged(nameof(ButtonIsHitTestVisible));
            }
        }

        public FeedTileViewModel(ISyndicationItem syndicationItem, string source, MainViewModel mainViewModel)
        {
            Item = syndicationItem;
            Title = syndicationItem.Title;
            Description = syndicationItem.Description;
            Source = source;
            FeedTileCommand = new FeedTileCommand(this, mainViewModel);
            ButtonIsHitTestVisible = true;
           // PubDate = syndicationItem.Published.ToString("MMMM dd yyyy");
        }
    }
}
