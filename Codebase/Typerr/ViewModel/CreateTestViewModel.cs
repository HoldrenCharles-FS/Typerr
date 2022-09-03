using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class CreateTestViewModel : ViewModelBase
    {
        public ICommand OpenFromFileCommand { get; }
        public ICommand GetTestCommand { get; }
        public ICommand CreateCommand { get; }

        public ICommand CreateTestCloseCommand { get; }

        private TestModel _testModel;
        public TestModel TestModel
        {
            get
            {
                return _testModel;
            }
            set
            {
                _testModel = value;
                OnPropertyChanged(nameof(TestModel));
            }
        }

        #region Test Properties
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

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get
            {
                return _publishDate;
            }
            set
            {
                _publishDate = value;
                OnPropertyChanged(nameof(PublishDate));
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

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        #endregion

        private string _uploadImagePrompt;
        public string UploadImagePrompt
        {
            get
            {
                return Image == null ? "Click to add an image" : "Click to change image";
            }
            set
            {
                _uploadImagePrompt = value;
                OnPropertyChanged(nameof(UploadImagePrompt));
            }
        }

        public CreateTestViewModel(ICommand openFromFileCommand, ICommand createTestCloseCommand)
        {
            OpenFromFileCommand = openFromFileCommand;
            GetTestCommand = new GetTestCommand(this);
            CreateCommand = new CreateCommand(this);
            CreateTestCloseCommand = createTestCloseCommand;
        }
    }
}
