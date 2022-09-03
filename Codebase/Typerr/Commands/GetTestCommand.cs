using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.ViewModel;
using TyperrDemo.Services;

namespace Typerr.Commands
{
    public class GetTestCommand : CommandBase
    {
        private readonly CreateTestViewModel _createTestViewModel;

        public TestModel TestModel { get; set; }

        public GetTestCommand(CreateTestViewModel createTestViewModel)
        {
            _createTestViewModel = createTestViewModel;
        }

        public override async void Execute(object parameter)
        {
            if (!Uri.IsWellFormedUriString(_createTestViewModel.TextArea, UriKind.Absolute))
                return;

            TestModel = await UrlService.GetTestByUrl(_createTestViewModel.TextArea);

            _createTestViewModel.TextArea = TestModel.article.text;
            _createTestViewModel.Title = TestModel.article.title;
            _createTestViewModel.Author = TestModel.article.author;
            _createTestViewModel.Source = TestModel.article.site_name;
            _createTestViewModel.PublishDate = TestModel.article.pub_date ?? DateTime.Now;
            _createTestViewModel.Summary = TestModel.article.summary;
        }
    }
}
