using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;
using Typerr.Stores;
using Typerr.ViewModel;

namespace Typerr
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public User User { get; set; }


        public App()
        {
            _navigationStore = new NavigationStore();
            User = UserService.Read();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            MainViewModel mainViewModel = new MainViewModel(_navigationStore);
            CreateTestTileCommand createTestTileCommand = new CreateTestTileCommand(mainViewModel);
            DialogCloseCommand createTestCloseCommand = new DialogCloseCommand(mainViewModel);
            GoToLibraryCommand goToLibraryCommand = new GoToLibraryCommand(_navigationStore);
            HomeViewModel homeViewModel = new HomeViewModel(mainViewModel, createTestTileCommand, goToLibraryCommand, User);
            CreateTestViewModel createTestViewModel = new CreateTestViewModel(createTestCloseCommand, homeViewModel);
            mainViewModel.CreateTestViewModel = createTestViewModel;

            LoadTests(homeViewModel);

            _navigationStore.CurrentViewModel = homeViewModel;
            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void LoadTests(HomeViewModel homeViewModel)
        {
            DirectoryInfo dir = new DirectoryInfo("tests");

            FileInfo[] files = dir.GetFiles();

            files = files.OrderBy(x => x.CreationTime).ToArray();

            if (files.Length > 0)
            {
                TestModel testModel = new TestModel();
                testModel.article = new Article();

                foreach (FileInfo file in files)
                {
                    if (file.FullName.EndsWith(".typr") && file.Length > 0)
                    {
                        using (FileStream fileStream = File.OpenRead(file.FullName))
                        {
                            using (XmlReader reader = XmlReader.Create(fileStream))
                            {
                                reader.MoveToFirstAttribute();
                                reader.ReadToFollowing("TestModel");
                                reader.MoveToFirstAttribute();

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

                                reader.ReadToFollowing("article");
                                reader.MoveToFirstAttribute();
                                testModel.article.title = reader.Value;

                                reader.MoveToNextAttribute();
                                testModel.article.text = TestService.FormatText(reader.Value);

                                testModel.WordCount = TestService.GetWordCount(testModel.article.text);

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



                            }
                        }

                        homeViewModel.AddLibTile(testModel);
                    }

                }
            }
        }
    }
}
