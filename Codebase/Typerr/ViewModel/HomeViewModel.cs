using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Typerr.Model;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly User _user;

        private ObservableCollection<LibTileViewModel> _libTileViewModels;

        public ObservableCollection<LibTileViewModel> LibTileViewModels
        {
            get { return _libTileViewModels; }
            set { _libTileViewModels = value; }
        }

        public MainViewModel MainViewModel { get; }
        public NavPanelViewModel NavPanelViewModel { get; set; }
        public ICommand CreateTestTileCommand { get; }
        public ICommand GoToLibraryCommand { get; }


        public HomeViewModel(MainViewModel mainViewModel, ICommand createTestTileCommand, ICommand goToLibraryCommand, User user)
        {
            MainViewModel = mainViewModel;
            CreateTestTileCommand = createTestTileCommand;
            GoToLibraryCommand = goToLibraryCommand;
            _libTileViewModels = new ObservableCollection<LibTileViewModel>();
            _user = user;
            RefreshLibrary();
        }

        public void RefreshLibrary()
        {
            IEnumerable<LibTileViewModel> result = MainViewModel.AllLibTileViewModels.Take(6);

            LibTileViewModels.Clear();

            foreach (var r in result)
            {
                LibTileViewModels.Add(r);
            }
        }
    }
}