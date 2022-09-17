using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Typerr.Commands;
using Typerr.Model;
using Typerr.Service;
using Typerr.View;

namespace Typerr.ViewModel
{
    public class CreateTestViewModel : ViewModelBase, INotifyDataErrorInfo
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
                ClearErrors(nameof(TextArea));

                if (value.Length > 10000)
                {
                    TextAreaBrush = new SolidColorBrush(Colors.Red);
                    AddError(nameof(TextArea), $"Tests cannot exceed 10,000 characters. Please delete {FormatService.FormatNumber(value.Length - 10000)} more characters.");
                }
                else
                {
                    TextAreaBrush = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
                }
                _textArea = value;
                OnPropertyChanged(nameof(TextArea));

                if (TestModel != null)
                {
                    TestModel.article.text = _textArea;
                }


                if (!string.IsNullOrWhiteSpace(_textArea) && _textArea != DefaultMessage)
                {
                    TextAreaToolTip = "Typerr will automatically format the text, from here you can remove any text you don't want, such as section headers.";
                    SidebarEnabled = true;
                    if (!string.IsNullOrWhiteSpace(Title) && _textArea.Length <= 10000)
                    {
                        CreateButtonEnabled = true;
                    }
                    else
                    {
                        CreateButtonEnabled = false;
                    }
                }
                else
                {
                    TextAreaToolTip = "Paste a URL here to generate a test(Ctrl + V or from the Right-Click Menu)";
                    SidebarEnabled = false;
                    CreateButtonEnabled = false;
                }
                if (Uri.IsWellFormedUriString(_textArea, UriKind.Absolute) || Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                {
                    GetTestButtonEnabled = true;
                    if (Uri.IsWellFormedUriString(_textArea, UriKind.Absolute) && !ObtainedUrl)
                    {
                        Url = _textArea;
                    }
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

                if (!string.IsNullOrWhiteSpace(_textArea) && _textArea != DefaultMessage && !string.IsNullOrWhiteSpace(_title) && _textArea.Length <= 10000)
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

        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
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
                if (value && ObtainedUrl)
                {
                    GetTestButtonToolTip = "Click here to reset the article.";
                }
                else if (value && !ObtainedUrl)
                {
                    GetTestButtonToolTip = "Click here to send a request to the Article and Data Extraction API.";
                }
                else
                {
                    GetTestButtonToolTip = "This button is disabled until a URL is provided.";
                }
                _getTestButtonEnabled = value;
                OnPropertyChanged(nameof(GetTestButtonEnabled));
            }
        }

        private int _httpResponseOk;
        public int HttpResponseOk
        {
            get
            {
                return _httpResponseOk;
            }
            set
            {
                _httpResponseOk = value;
                ClearErrors(nameof(TextArea));
                if (_httpResponseOk == 0)
                {
                    TextAreaBrush = new SolidColorBrush(Colors.Red);
                    AddError(nameof(TextArea), "Sorry, the request failed to return any data. You can try a different URL or copy and paste the text itself.");
                }
                else
                {
                    TextAreaBrush = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
                }

                OnPropertyChanged(nameof(HttpResponseOk));
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
                if (value)
                {
                    CreateTestButtonToolTip = "Click here to create your new test.";
                }
                else
                {
                    CreateTestButtonToolTip = "This button is disabled until a title and body are provided.";
                }
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

        private SolidColorBrush _textAreaBrush;
        public SolidColorBrush TextAreaBrush
        {
            get
            {
                return _textAreaBrush;
            }
            set
            {
                _textAreaBrush = value;
                OnPropertyChanged(nameof(TextAreaBrush));
            }
        }

        private string _textAreaToolTip;
        public string TextAreaToolTip
        {
            get
            {
                return _textAreaToolTip;
            }
            set
            {
                _textAreaToolTip = value;
                OnPropertyChanged(nameof(TextAreaToolTip));
            }
        }

        private string _createTestButtonToolTip;
        public string CreateTestButtonToolTip
        {
            get
            {
                return _createTestButtonToolTip;
            }
            set
            {
                _createTestButtonToolTip = value;
                OnPropertyChanged(nameof(CreateTestButtonToolTip));
            }
        }

        private string _getTestButtonToolTip;


        public string GetTestButtonToolTip
        {
            get
            {
                return _getTestButtonToolTip;
            }
            set
            {
                _getTestButtonToolTip = value;
                OnPropertyChanged(nameof(GetTestButtonToolTip));
            }
        }

        public bool ObtainedUrl { get; set; } = false;

        public static string DefaultMessage { get; } = "Paste a URL here or begin typing to create your test";

        // Number obtained from actualheight property from the side column single row textbox's default height
        // Variable is introduced to force those fields to stay at that height.
        public double SingleRowHeight { get; } = 40.620000000000005;

        private readonly Dictionary<string, List<string>> _propertyErrors;

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public CreateTestViewModel(ICommand createTestCloseCommand, HomeViewModel homeViewModel)
        {
            OpenFromFileCommand = new OpenFromFileCommand(this);
            GetTestCommand = new GetTestCommand(this);
            CreateCommand = new CreateCommand(this, homeViewModel);
            CreateTestCloseCommand = createTestCloseCommand;
            RemoveImageCommand = new RemoveImageCommand(this);
            AddImageCommand = new AddImageCommand(this);
            _propertyErrors = new Dictionary<string, List<string>>();
            Init();
        }

        private void Init()
        {
            TextArea = DefaultMessage;
            PublishDate = null;
            SidebarEnabled = false;
            GetTestButtonEnabled = false;
            CreateButtonEnabled = false;
            TestModel = new TestModel();
            TextAreaBrush = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
            TextAreaToolTip = "Paste a URL here to generate a test(Ctrl + V or from the Right-Click Menu)";
            HttpResponseOk = -1;
        }

        public void Reset()
        {
            TestModel = null;
            TextArea = DefaultMessage;
            Title = "";
            Author = "";
            Summary = "";
            Source = "";
            Image = null;
            PublishDate = null;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            
            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyErrors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}