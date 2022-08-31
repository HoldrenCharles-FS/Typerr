using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Typerr.Commands;

namespace Typerr.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand HomeViewCommand { get; }
    }
}