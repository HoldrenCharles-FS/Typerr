using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Typerr.Model;

namespace Typerr.Service
{
    public class UserService
    {
        public static void Write(User user)
        {
            if (File.Exists("user"))
            {
                try
                {
                    File.Delete("user");
                }
                catch (Exception)
                {

                    throw;
                }

                string requestTimes = "";

                for (int i = 0; i < user.RequestTimes.Count; i++)
                {
                    requestTimes += user.RequestTimes[i].ToString();
                    if (i != user.RequestTimes.Count - 1)
                    {
                        requestTimes += ",";
                    }
                }

                string subscriptions = "";

                for (int i = 0; i < user.Subscriptions.Count; i++)
                {
                    subscriptions += user.Subscriptions[i].ToString();
                    if (i != user.Subscriptions.Count - 1)
                    {
                        subscriptions += ",";
                    }
                }

                FileStream writer = new FileStream(@"user", FileMode.CreateNew);

                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("User");
                    xmlWriter.WriteAttributeString("RecentWPM", user.RecentWpm.ToString());
                    xmlWriter.WriteAttributeString("Mode", user.Mode.ToString());
                    xmlWriter.WriteAttributeString("Minutes", user.Minutes.ToString());
                    xmlWriter.WriteAttributeString("RequestTimes", requestTimes);
                    xmlWriter.WriteAttributeString("Subscriptions", subscriptions);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    writer.Flush();
                }

                writer.Close();
            }
            else
            {
                CreateUser();
            }
        }

        public static User Read()
        {
            if (File.Exists("user"))
            {
                using (FileStream fileStream = File.OpenRead("user"))
                {
                    using (XmlReader reader = XmlReader.Create(fileStream))
                    {
                        int result = 0;
                        int recentWpm = 33;
                        int mode = 0;
                        int minutes = 3;
                        List<DateTime> requestTimes = new List<DateTime>();
                        List<string> subscriptions = new List<string>();
                        reader.MoveToFirstAttribute();
                        reader.ReadToFollowing("User");
                        reader.MoveToFirstAttribute();
                        if (int.TryParse(reader.Value, out result))
                        {
                            recentWpm = result;
                        }
                        reader.MoveToNextAttribute();
                        if (int.TryParse(reader.Value, out result))
                        {
                            mode = result;
                        }
                        reader.MoveToNextAttribute();
                        if (int.TryParse(reader.Value, out result))
                        {
                            minutes = result;
                        }
                        
                        reader.MoveToNextAttribute();
                        string[] requestTimesData = reader.Value.Split(',');

                        if (requestTimesData.Length != 1 && requestTimesData[0] != "")
                        {
                            foreach (var request in requestTimesData)
                            {
                                if ((DateTime.Now - DateTime.Parse(request)).Days <= 30)
                                {
                                    requestTimes.Add(DateTime.Parse(request));
                                }
                            }
                        }

                        reader.MoveToNextAttribute();
                        string[] subscriptionsData = reader.Value.Split(',');

                        if (subscriptionsData.Length != 1 && subscriptionsData[0] != "")
                        {
                            foreach (var subscription in subscriptionsData)
                            {
                                subscriptions.Add(subscription);
                            }
                        }


                        return new User(recentWpm, mode, minutes, requestTimes, subscriptions);
                    }
                }

            }
            else
            {
                return CreateUser();
            }
        }

        public static User CreateUser()
        {
            FileStream writer = new FileStream(@"user", FileMode.CreateNew);

            using (XmlWriter xmlWriter = XmlWriter.Create(writer))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("User");
                xmlWriter.WriteAttributeString("RecentWPM", "33");
                xmlWriter.WriteAttributeString("Mode", "0");
                xmlWriter.WriteAttributeString("Minutes", "3");
                xmlWriter.WriteAttributeString("RequestTimes", "");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();

            return new User(33, 0, 3, new List<DateTime>(), new List<string>());
        }
    }
}
