using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Typerr.Model;
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

            _createTestViewModel.TextArea = _createTestViewModel.TestModel.article.text;
            _createTestViewModel.Title = _createTestViewModel.TestModel.article.title;
            _createTestViewModel.Author = _createTestViewModel.TestModel.article.author;
            _createTestViewModel.Source = _createTestViewModel.TestModel.article.site_name;
            _createTestViewModel.PublishDate = _createTestViewModel.TestModel.article.pub_date ?? DateTime.Now;
            _createTestViewModel.Summary = _createTestViewModel.TestModel.article.summary;
            _createTestViewModel.Image = new BitmapImage(new Uri(_createTestViewModel.TestModel.article.image));
            _createTestViewModel.UploadImagePrompt = _createTestViewModel.UploadImagePrompt;
        }
    }
}
