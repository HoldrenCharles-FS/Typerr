using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;
using Typerr.Model;

namespace Typerr.Service
{
    public class RssService
    {
        public static async Task<RssModel> Read(string filePath)
        {
            RssModel rssModel = new RssModel();

            using (XmlReader xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
            {
                RssFeedReader feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            ISyndicationImage syndicationImage = await feedReader.ReadImage();

                            rssModel.Image = new BitmapImage(syndicationImage.Url);

                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            rssModel.Items.Add(item);
                            break;

                        // Read link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                             rssModel.Uri = link.Uri.AbsoluteUri;
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();

                            switch (content.Name)
                            {
                                case "title":
                                    rssModel.Title = content.Value;
                                    break;
                                case "description":
                                    rssModel.Description = content.Value;
                                    break;
                                case "lastBuildDate":
                                 rssModel.LastBuildDate = DateTime.Parse(content.Value);
                                    break;
                                case "pubDate":
                                    rssModel.PubDate = DateTime.Parse(content.Value);
                                    break;
                            }

                            break;
                    }
                }
            }

            return rssModel;
        }
    }
}
