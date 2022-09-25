using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class ItemPreviewViewModel : ViewModelBase
    {
        public FeedTileViewModel FeedTileViewModel { get; }
        public ICommand DialogCloseCommand { get; }
        public ICommand GenerateTestCommand { get; }
        public ICommand OpenLinkCommand { get; }

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

        private double _pubDateHeight;
        public double PubDateHeight
        {
            get
            {
                return _pubDateHeight;
            }
            set
            {
                _pubDateHeight = value;
                OnPropertyChanged(nameof(PubDateHeight));
            }
        }

        public string URL { get; internal set; }

        private TextBlock _link;
        public TextBlock Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                OnPropertyChanged(nameof(Link));
            }
        }

        public ItemPreviewViewModel(FeedTileViewModel feedTileViewModel, MainViewModel mainViewModel)
        {
            FeedTileViewModel = feedTileViewModel;
            DialogCloseCommand = new DialogCloseCommand(mainViewModel);
            GenerateTestCommand = new GenerateTestCommand(this, mainViewModel);
            OpenLinkCommand = new OpenLinkCommand(this);
            Init();
        }

        private void Init()
        {
            Title = FeedTileViewModel.Title;
            Description = FeedTileViewModel.Description;
            DescriptionHeight = string.IsNullOrWhiteSpace(Description) ? 0 : 80;
            PubDate = (FeedTileViewModel.Item.Published.DateTime == DateTime.MinValue) ? "" : "Publish Date: " + FeedTileViewModel.Item.Published.ToString("MMM dd yyyy");
            PubDateHeight = string.IsNullOrWhiteSpace(PubDate) ? 0 : 30;
            Link = new TextBlock();
            string alternateLink = "";
            foreach (ISyndicationLink item in FeedTileViewModel.Item.Links)
            {
                alternateLink = item.Uri.AbsoluteUri;
            }

            if (Uri.IsWellFormedUriString(FeedTileViewModel.Item.Id, UriKind.Absolute))
            {
                URL = FeedTileViewModel.Item.Id.ToString();
                
            }
            else if (Uri.IsWellFormedUriString(alternateLink, UriKind.Absolute))
            {
                URL = alternateLink;
            }
            Hyperlink hyperlink = new Hyperlink(new Run(URL));
            hyperlink.Command = OpenLinkCommand;
            Link.Inlines.Clear();
            Link.Inlines.Add(new Run("Link: "));
            Link.Inlines.Add(hyperlink);
            Link.Padding = new Thickness(10, 0, 10, 0);
            Link.Margin = new Thickness(10, 0, 10, 0);
            Link.FontSize = 16;

        }
    }
}
