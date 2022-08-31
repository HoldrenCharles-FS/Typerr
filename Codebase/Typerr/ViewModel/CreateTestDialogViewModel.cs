using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Typerr.ViewModel
{
    public class CreateTestDialogViewModel : ViewModelBase
    {
        public ICommand OpenFromFileCommand { get; }
        public ICommand GetTestCommand { get; }
        public ICommand CreateCommand { get; }

        private string textArea;
        public string TextArea
        {
            get
            {
                return textArea;
            }
            set
            {
                textArea = value;
                OnPropertyChanged(nameof(TextArea));
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _author;
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private string _source;
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        private string _summary;
        public string Summary
        {
            get
            {
                return _summary;
            }
            set
            {
                _summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }
    }
}
