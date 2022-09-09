using System;
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
        public ICommand RemoveImageCommand { get; }
        public ICommand AddImageCommand { get; }

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
        private string _textArea;
        public string TextArea
        {
            get
            {
                return _textArea;
            }
            set
            {
                _textArea = value;
                OnPropertyChanged(nameof(TextArea));

                if (TestModel != null)
                {
                    TestModel.article.text = _textArea;
                }


                if (!string.IsNullOrWhiteSpace(_textArea) && _textArea != DefaultMessage)
                {
                    SidebarEnabled = true;
                    if (!string.IsNullOrWhiteSpace(Title))
                    {
                        CreateButtonEnabled = true;
                    }
                }
                else
                {
                    SidebarEnabled = false;
                    CreateButtonEnabled = false;
                }
                if (Uri.IsWellFormedUriString(_textArea, UriKind.Absolute))
                {
                    GetTestButtonEnabled = true;
                }
                else
                {
                    GetTestButtonEnabled = false;
                }
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
                if (TestModel != null)
                {
                    TestModel.article.title = _title;
                }

                if (!string.IsNullOrWhiteSpace(_textArea) && _textArea != DefaultMessage && !string.IsNullOrWhiteSpace(_title))
                {
                    CreateButtonEnabled = true;
                }
                else
                {
                    CreateButtonEnabled = false;
                }

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

                if (TestModel != null)
                {
                    TestModel.article.author = _author;
                }


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
                if (TestModel != null)
                {
                    TestModel.article.site_name = _source;
                }

            }
        }

        private DateTime? _publishDate;
        public DateTime? PublishDate
        {
            get
            {
                return _publishDate;
            }
            set
            {
                _publishDate = value;
                OnPropertyChanged(nameof(PublishDate));

                if (TestModel != null)
                {
                    TestModel.article.pub_date = _publishDate;
                }
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

                if (TestModel != null)
                {
                    _testModel.article.summary = _summary;
                }

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

        private bool _sidebarEnabled;
        public bool SidebarEnabled
        {
            get
            {
                return _sidebarEnabled;
            }
            set
            {
                _sidebarEnabled = value;
                OnPropertyChanged(nameof(SidebarEnabled));

                if (_sidebarEnabled == true)
                {
                    ForegroundColor = "#000";
                    DeleteForeground = "#F43";
                }
                else
                {
                    ForegroundColor = "#888";
                    DeleteForeground = "#FBA";
                }
            }
        }

        private bool _getTestButtonEnabled;
        public bool GetTestButtonEnabled
        {
            get
            {
                return _getTestButtonEnabled;
            }
            set
            {
                _getTestButtonEnabled = value;
                OnPropertyChanged(nameof(GetTestButtonEnabled));
            }
        }

        private bool _createButtonEnabled;
        public bool CreateButtonEnabled
        {
            get
            {
                return _createButtonEnabled;
            }
            set
            {
                _createButtonEnabled = value;
                OnPropertyChanged(nameof(CreateButtonEnabled));
            }
        }

        private string _foregroundColor;
        public string ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                _foregroundColor = value;
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }

        private string _deleteForeground;
        public string DeleteForeground
        {
            get
            {
                return _deleteForeground;
            }
            set
            {
                _deleteForeground = value;
                OnPropertyChanged(nameof(DeleteForeground));
            }
        }

        public static string DefaultMessage { get; } = "Paste a URL here or begin typing to create your test";

        // Number obtained from actualheight property from the side column single row textbox's default height
        // Variable is introduced to force those fields to stay at that height.
        public double SingleRowHeight { get; } = 40.620000000000005;

        public CreateTestViewModel(ICommand createTestCloseCommand, HomeViewModel homeViewModel)
        {
            OpenFromFileCommand = new OpenFromFileCommand(this);
            GetTestCommand = new GetTestCommand(this);
            CreateCommand = new CreateCommand(this, homeViewModel);
            CreateTestCloseCommand = createTestCloseCommand;
            RemoveImageCommand = new RemoveImageCommand(this);
            AddImageCommand = new AddImageCommand(this);
            TextArea = DefaultMessage;
            PublishDate = null;
            SidebarEnabled = false;
            GetTestButtonEnabled = false;
            TestModel = new TestModel();
        }
    }
}
