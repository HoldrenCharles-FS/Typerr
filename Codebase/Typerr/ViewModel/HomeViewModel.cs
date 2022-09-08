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

        private readonly ObservableCollection<LibTileViewModel> _allLibTileViewModels;

        public IEnumerable<LibTileViewModel> AllLibTileViewModels => _allLibTileViewModels;

        private ObservableCollection<LibTileViewModel> _libTileViewModels;

        public ObservableCollection<LibTileViewModel> LibTileViewModels
        {
            get { return _libTileViewModels; }
            set { _libTileViewModels = value; }
        }

        public ICommand CreateTestTileCommand { get; }
        public ICommand GoToLibraryCommand { get; }


        public HomeViewModel(ICommand createTestTileCommand, ICommand goToLibraryCommand, User user)
        {
            CreateTestTileCommand = createTestTileCommand;
            GoToLibraryCommand = goToLibraryCommand;
            _allLibTileViewModels = new ObservableCollection<LibTileViewModel>();
            _libTileViewModels = new ObservableCollection<LibTileViewModel>();
            _user = user;
        }

        public void AddLibTile(TestModel testModel)
        {
            LibTileViewModel libTileViewModel = new LibTileViewModel(testModel, _user);

            _allLibTileViewModels.Insert(0, libTileViewModel);
           

            IEnumerable<LibTileViewModel> result = _allLibTileViewModels.Take(6);

            LibTileViewModels.Clear();

            foreach (var r in result)
            {
                LibTileViewModels.Add(r);
            }
        }

        public void HomeLibraryContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}