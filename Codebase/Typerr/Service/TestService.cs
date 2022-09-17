using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;
using Typerr.Model;

namespace Typerr.Service
{
    public class TestService
    {
        public static void Write(TestModel testModel, string filename = null)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                filename = testModel.Filename;
            }
            else
            {
                // Assign the newly created tests filename to the paramater passed in
                testModel.Filename = filename;
            }

            try
            {
                File.Delete(filename);
            }
            catch (Exception)
            {
                throw;
            }

            

            string errorPositions = "NULL";

            if (testModel.testData.ErrorPositions.Count == 1)
            {
                errorPositions = testModel.testData.ErrorPositions[0].ToString();
            }
            else if (testModel.testData.ErrorPositions.Count > 0)
            {
                errorPositions = "";
                for (int i = 0; i < testModel.testData.ErrorPositions.Count; i++)
                {
                    errorPositions += testModel.testData.ErrorPositions[i].ToString();
                    if (i != testModel.testData.ErrorPositions.Count - 1)
                    {
                        errorPositions += ",";
                    }
                }
            }

            FileStream writer = new FileStream(filename, FileMode.CreateNew);

            using (XmlWriter xmlWriter = XmlWriter.Create(writer))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("TestModel");
                xmlWriter.WriteAttributeString("Image", string.IsNullOrWhiteSpace(testModel.Base64Image) ? "NULL" : testModel.Base64Image);
                xmlWriter.WriteStartElement("article");
                xmlWriter.WriteAttributeString("title", testModel.article.title);
                xmlWriter.WriteAttributeString("text", testModel.article.text);
                xmlWriter.WriteAttributeString("summary", testModel.article.summary);
                xmlWriter.WriteAttributeString("author", testModel.article.author);
                xmlWriter.WriteAttributeString("site_name", testModel.article.site_name);
                xmlWriter.WriteAttributeString("canonical_url", testModel.article.canonical_url);
                xmlWriter.WriteAttributeString("pub_date", testModel.article.pub_date.ToString());
                xmlWriter.WriteAttributeString("image", testModel.article.image);
                xmlWriter.WriteAttributeString("favicon", testModel.article.favicon);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("testData");
                xmlWriter.WriteAttributeString("TestStarted", testModel.testData.TestStarted.ToString());
                xmlWriter.WriteAttributeString("LastPosition", testModel.testData.LastPosition.ToString());
                xmlWriter.WriteAttributeString("ErrorPositions", errorPositions);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();
        }

        public static TestModel Read(string filename)
        {
            TestModel testModel = new TestModel();
            testModel.article = new Article();

            using (FileStream fileStream = File.OpenRead(filename))
            {
                using (XmlReader reader = XmlReader.Create(fileStream))
                {
                    reader.MoveToFirstAttribute();
                    reader.ReadToFollowing("TestModel");

                    reader.MoveToFirstAttribute();

                    if (reader.Value == "NULL")
                    {
                        testModel.Image = null;
                    }
                    else
                    {
                        byte[] bytes = Convert.FromBase64String(reader.Value);
                        MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length);
                        memoryStream.Write(bytes, 0, bytes.Length);
                        Image image = Image.FromStream(memoryStream, true);

                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memStream2 = new MemoryStream())
                        {
                            image.Save(memStream2, System.Drawing.Imaging.ImageFormat.Png);
                            memStream2.Position = 0;

                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.UriSource = null;
                            bitmapImage.StreamSource = memStream2;
                            bitmapImage.EndInit();
                        }
                        memoryStream.Close();
                        testModel.Image = bitmapImage;
                    }

                    reader.ReadToFollowing("article");
                    reader.MoveToFirstAttribute();
                    testModel.article.title = reader.Value;

                    reader.MoveToNextAttribute();
                    testModel.article.text = FormatService.FormatText(reader.Value);

                    testModel.WordCount = FormatService.GetWordCount(testModel.article.text);

                    reader.MoveToNextAttribute();
                    testModel.article.summary = reader.Value;

                    reader.MoveToNextAttribute();
                    testModel.article.author = reader.Value;

                    reader.MoveToNextAttribute();
                    testModel.article.site_name = reader.Value;

                    reader.MoveToNextAttribute();
                    testModel.article.canonical_url = reader.Value;

                    reader.MoveToNextAttribute();
                    if (string.IsNullOrEmpty(reader.Value) || !DateTime.TryParse(reader.Value, out DateTime result))
                    {
                        testModel.article.pub_date = null;
                    }
                    else
                    {
                        testModel.article.pub_date = DateTime.Parse(reader.Value);
                    }


                    reader.MoveToNextAttribute();
                    testModel.article.image = reader.Value;

                    reader.MoveToNextAttribute();
                    testModel.article.favicon = reader.Value;

                    reader.ReadToFollowing("testData");
                    reader.MoveToFirstAttribute();
                    testModel.testData.TestStarted = bool.Parse(reader.Value);

                    reader.MoveToNextAttribute();
                    testModel.testData.LastPosition = int.Parse(reader.Value);

                    reader.MoveToNextAttribute();
                    string[] errorPositions = reader.Value.Split(',');

                    testModel.testData.ErrorPositions = new List<int>();
                    foreach (string pos in errorPositions)
                    {
                        if (pos == "NULL")
                            break;
                        testModel.testData.ErrorPositions.Add(int.Parse(pos));
                    }

                    testModel.Filename = filename;
                }
            }


            return testModel;
        }
    }
}
