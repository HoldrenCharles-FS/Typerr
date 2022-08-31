using System;
using System.Collections.Generic;
using System.Text;
using Typerr.ViewModel;

namespace Typerr.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel; 
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChaged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChaged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
