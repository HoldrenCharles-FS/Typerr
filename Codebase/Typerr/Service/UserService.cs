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
                FileStream writer = new FileStream(@"user", FileMode.CreateNew);

                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("User");
                    xmlWriter.WriteAttributeString("RecentWPM", user.RecentWpm.ToString());
                    xmlWriter.WriteAttributeString("Mode", user.Mode.ToString());
                    xmlWriter.WriteAttributeString("Minutes", user.Minutes.ToString());
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
                        return new User(recentWpm, mode, minutes);
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
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();

            return new User(33, 0, 3);
        }
    }
}
