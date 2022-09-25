using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Typerr.Model
{
    public class RssModel
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public string Uri { get; set; }
        public DateTime LastBuildDate { get; set; }
        public DateTime PubDate { get; set; }
        public List<ISyndicationItem> Items { get; set; }
        

        public RssModel()
        {
            Items = new List<ISyndicationItem>();
        }
    }
}
