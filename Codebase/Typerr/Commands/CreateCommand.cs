using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;
using Typerr.ViewModel;
using System.Drawing.Imaging;

namespace Typerr.Commands
{
    public class CreateCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;
        private readonly HomeViewModel _homeViewModel;

        public CreateCommand(CreateTestViewModel createTestViewModel, HomeViewModel homeViewModel)
        {
            _createTestViewModel = createTestViewModel;
            _homeViewModel = homeViewModel;
        }

        public override void Execute(object parameter)
        {
            FileStream writer = new FileStream(GenerateFileName(), FileMode.CreateNew);
            string image = "NULL";
            if (_createTestViewModel.Image != null)
            {
                _createTestViewModel.TestModel.Image = _createTestViewModel.Image;
                image = CompressImage();
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(writer))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("TestModel");
                xmlWriter.WriteAttributeString("Image", image);
                xmlWriter.WriteStartElement("article");
                xmlWriter.WriteAttributeString("title", _createTestViewModel.TestModel.article.title);
                xmlWriter.WriteAttributeString("text", _createTestViewModel.TestModel.article.text);
                xmlWriter.WriteAttributeString("summary", _createTestViewModel.TestModel.article.summary);
                xmlWriter.WriteAttributeString("author", _createTestViewModel.TestModel.article.author);
                xmlWriter.WriteAttributeString("site_name", _createTestViewModel.TestModel.article.site_name);
                xmlWriter.WriteAttributeString("canonical_url", _createTestViewModel.TestModel.article.canonical_url);
                xmlWriter.WriteAttributeString("pub_date", _createTestViewModel.TestModel.article.pub_date.ToString());
                xmlWriter.WriteAttributeString("image", _createTestViewModel.TestModel.article.image);
                xmlWriter.WriteAttributeString("favicon", _createTestViewModel.TestModel.article.favicon);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("testData");
                xmlWriter.WriteAttributeString("TestStarted", _createTestViewModel.TestModel.testData.TestStarted.ToString());
                xmlWriter.WriteAttributeString("LastPosition", _createTestViewModel.TestModel.testData.LastPosition.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();

            _homeViewModel.AddLibTile(_createTestViewModel.TestModel);

            // TODO: Reset pubdate and image
            _createTestViewModel.CreateTestCloseCommand.Execute(parameter);
            _createTestViewModel.TextArea = CreateTestViewModel.DefaultMessage;
            _createTestViewModel.Title = "";
            _createTestViewModel.Author = "";
            _createTestViewModel.Summary = "";
            _createTestViewModel.Source = "";

            

        }

        private string CompressImage()
        {
            const string temp = "img";
            const int width = 500;
            const int height = 300;

            if (File.Exists(temp))
            {
                try
                {
                    File.Delete(temp);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            File.Create(temp).Close();

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_createTestViewModel.Image));

            using (var fileStream = new FileStream(temp, FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            Bitmap oldImage = (Bitmap)Bitmap.FromFile(temp);


            float aspectWidth = 5;
            float aspectHeight = 3;
            
            RectangleF rect = new RectangleF(0, 0, oldImage.Width, oldImage.Height);

            if (rect.Width / rect.Height > (aspectWidth / aspectHeight) + 0.005f)
            {
                // Picture is landscape
                rect.Width = (float)rect.Height * (aspectWidth / aspectHeight);

            }
            else if (rect.Width / rect.Height < (aspectWidth / aspectHeight) - 0.005f)
            {
                // Picture is Portrait
                rect.Height = rect.Width / (aspectWidth / aspectHeight);

            }

            Image image = oldImage.Clone(rect, oldImage.PixelFormat);

            oldImage.Dispose();
            File.Delete(temp);

            image = new Bitmap(image, width, height);

            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                _createTestViewModel.Image = bitmapImage;

                byte[] byteImage = memory.ToArray();
                return Convert.ToBase64String(byteImage);
            }
        }

        private string GenerateFileName()
        {
            // String formatting
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string filename = textInfo.ToLower(_createTestViewModel.TestModel.article.title);
            filename = filename.Replace(" ", "_");

            // Remove restricted chars
            filename = filename.Replace(":", string.Empty);
            filename = filename.Replace("|", string.Empty);
            filename = filename.Replace(@"\", string.Empty);
            filename = filename.Replace("/", string.Empty);
            filename = filename.Replace("?", string.Empty);
            filename = filename.Replace(">", string.Empty);
            filename = filename.Replace("<", string.Empty);
            filename = filename.Replace("*", string.Empty);
            filename = filename.Replace("\"", string.Empty);

            if (filename.Length > 15)
            {
                // truncate file name
                filename = filename.Substring(0, 15);
            }

            // Append extension
            filename += ".typr";

            // Insert path
            filename = filename.Insert(0, "tests/");

            if (!Directory.Exists("tests"))
            {
                Directory.CreateDirectory("tests");
            }

            if (File.Exists(filename))
            {
                int i = 1;
                filename = filename.Insert(filename.Length - 5, "_1");
                while (File.Exists(filename))
                {
                    i++;
                    filename = filename.Remove(filename.Length - 6, 1);
                    filename = filename.Insert(filename.Length - 5, i.ToString());
                }
            }

            return filename;
        }
    }
}
