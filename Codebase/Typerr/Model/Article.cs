using System;

namespace Typerr.Model
{
    public class Article
    {
        // Article object from API
        public string title { get; set; }
        public string text { get; set; }
        public string summary { get; set; }
        public string author { get; set; }
        public string site_name { get; set; }
        public string canonical_url { get; set; }
        public DateTime? pub_date { get; set; }
        public string image { get; set; }
        public string favicon { get; set; }

        public Article()
        {

        }
    }
}
