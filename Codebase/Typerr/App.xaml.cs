using System;
using System.Collections.Generic;
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
            MainViewModel mainViewModel = new MainViewModel(_navigationStore, User);
            CreateTestTileCommand createTestTileCommand = new CreateTestTileCommand(mainViewModel);
            DialogCloseCommand createTestCloseCommand = new DialogCloseCommand(mainViewModel);
            NavigationCommand goToLibraryCommand = new NavigationCommand(_navigationStore, new LibraryViewModel(mainViewModel), mainViewModel, NavigationOption.None);
            NavigationCommand goToLibraryButtonCommand = new NavigationCommand(_navigationStore, new LibraryViewModel(mainViewModel), mainViewModel, NavigationOption.GoToLibraryButton);
            HomeViewModel homeViewModel = new HomeViewModel(mainViewModel, createTestTileCommand, goToLibraryButtonCommand, User);
            mainViewModel.SetHomeViewModel(homeViewModel);
            NavigationCommand goToHomeCommand = new NavigationCommand(_navigationStore, homeViewModel, mainViewModel, NavigationOption.None);
            CreateTestViewModel createTestViewModel = new CreateTestViewModel(createTestCloseCommand, homeViewModel);
            mainViewModel.CreateTestViewModel = createTestViewModel;
            NavPanelViewModel navPanelViewModel = new NavPanelViewModel(goToHomeCommand, goToLibraryCommand);
            mainViewModel.SetNavPanelViewModel(navPanelViewModel);
            mainViewModel.CurrentPanel = mainViewModel.NavPanelViewModel;
            homeViewModel.NavPanelViewModel = navPanelViewModel;

            LoadTests(mainViewModel, homeViewModel);

            _navigationStore.CurrentViewModel = homeViewModel;
            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void LoadTests(MainViewModel mainViewModel, HomeViewModel homeViewModel)
        {
            DirectoryInfo dir = new DirectoryInfo("tests");

            FileInfo[] files = dir.GetFiles();

            files = files.OrderBy(x => x.CreationTime).ToArray();

            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    TestModel testModel = new TestModel();
                    testModel.article = new Article();

                    if (file.FullName.EndsWith(".typr") && file.Length > 0)
                    {
                        testModel = TestService.Read(file.FullName);

                        mainViewModel.AddLibTile(testModel, homeViewModel);
                    }
                    else if (file.Length == 0)
                    {
                        // The file creation was interupted and has a size of zero, clean it up
                        File.Delete(file.FullName);
                    }

                }

                homeViewModel.RefreshLibrary();
            }
        }
    }
}
