using System;
using System.Windows.Media.Imaging;
using Typerr.Service;
using Typerr.ViewModel;
using TyperrDemo.Services;

namespace Typerr.Commands
{
    public class GetTestCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;

        public GetTestCommand(CreateTestViewModel createTestViewModel)
        {
            _createTestViewModel = createTestViewModel;
        }

        public override async void Execute(object parameter)
        {
            if (!Uri.IsWellFormedUriString(_createTestViewModel.TextArea, UriKind.Absolute))
                return;

            _createTestViewModel.TestModel = await UrlService.GetTestByUrl(_createTestViewModel.TextArea);

            _createTestViewModel.TestModel.article.text = TestService.FormatText(_createTestViewModel.TestModel.article.text);
            _createTestViewModel.TestModel.WordCount = TestService.GetWordCount(_createTestViewModel.TestModel.article.text);

            _createTestViewModel.TextArea = _createTestViewModel.TestModel.article.text;
            _createTestViewModel.Title = _createTestViewModel.TestModel.article.title;
            _createTestViewModel.Author = _createTestViewModel.TestModel.article.author;
            _createTestViewModel.Source = _createTestViewModel.TestModel.article.site_name;
            _createTestViewModel.PublishDate = _createTestViewModel.TestModel.article.pub_date ?? DateTime.Now;
            _createTestViewModel.Summary = _createTestViewModel.TestModel.article.summary;
            _createTestViewModel.Image = new BitmapImage(new Uri(_createTestViewModel.TestModel.article.image));

            // Refresh the Prompt Message
            _createTestViewModel.UploadImagePrompt = _createTestViewModel.UploadImagePrompt;
        }

        //// Calculates Word Count
        //private int GetWordCount()
        //{
        //    // We need to format the text beforehand
        //    FormatText();

        //    char[] delimiters = new char[] { ' ', '\r', '\n' };
        //    int wordCount = _createTestViewModel.TestModel.article.text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

        //    return wordCount;
        //}

        //private void FormatText()
        //{
        //    string text = _createTestViewModel.TestModel.article.text;

        //    _createTestViewModel.TestModel.article.text.Replace("—", string.Empty);

        //    while (text.Contains("  ")) text = text.Replace("  ", " ");

        //    _createTestViewModel.TestModel.article.text = text;
        //}
    }
}
