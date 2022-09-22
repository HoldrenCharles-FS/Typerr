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

        public RssModel(string title, string source, string description, BitmapImage image)
        {
            Title = title;
            Source = source;
            Description = description;
            Image = image;
        }
    }
}
