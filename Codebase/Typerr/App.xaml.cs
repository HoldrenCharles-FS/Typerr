using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Typerr.Model;
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
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public App()
        {
            _navigationStore = new NavigationStore();
            GetUser();
        }

        private void GetUser()
        {
            if (File.Exists("user"))
            {
                using (FileStream fileStream = File.OpenRead("user"))
                {
                    using (XmlReader reader = XmlReader.Create(fileStream))
                    {


                        reader.MoveToContent();
                        var data = reader.ReadElementContentAsString();

                        _user = new User(int.Parse(data));
                    }
                }

                Current.Properties["User"] = _user;
            } 
            else
            {
                CreateUser();
            }
        }

        private void CreateUser()
        {
            FileStream writer = new FileStream(@"user", FileMode.CreateNew);

            using (XmlWriter xmlWriter = XmlWriter.Create(writer))
            {
                xmlWriter.WriteElementString("RecentWpm", "33");

                xmlWriter.WriteEndDocument();

                writer.Flush();
            }

            writer.Close();

            _user = new User(33);

            Current.Properties["User"] = _user;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new HomeViewModel();

            MainWindow = new MainWindow() 
            { 
                DataContext = new MainViewModel(_navigationStore) 
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
