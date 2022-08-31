using System;
using System.Collections.Generic;
using System.Text;
using Typerr.Model;
using Typerr.ViewModel;

namespace Typerr.View
{
    public class LibTileViewModel : ViewModelBase
    {
        private LibTileModel _model;
        public string Title => _model.Title;
        public string AuthorName => _model.AuthorName;
        public string WebsiteName => _model.WebsiteName;
        public string WordCount { get; private set; }
        public string TimeRemaining { get; private set; }

        public LibTileViewModel(LibTileModel model)
        {
            _model = model;
            FormatWordCountAndTimeRemaining();
        }

        public LibTileViewModel()
        {
            _model = new LibTileModel();
            FormatWordCountAndTimeRemaining();
        }

        private void FormatWordCountAndTimeRemaining()
        {
            string wordCount = _model.WordCount.ToString();

            for (int i = wordCount.Length, j = 0; i > 0; i--, j++)
            {
                if (j % 3 == 0 && j != 0)
                {
                    wordCount = wordCount.Insert(i, ",");
                }
            }

            WordCount = wordCount;

            string timeRemaining = "";

            if (_model.TimeRemaining < 1 && _model.TimeRemaining > 0)
            {
                timeRemaining = _model.TimeRemaining * 60 + "s";
            }
            else if (_model.TimeRemaining > 43830) 
            {
                timeRemaining = Math.Round((double)_model.TimeRemaining / 43830, 1) + "mo";
            }
            else if (_model.TimeRemaining > 1440)
            {
                timeRemaining = Math.Round((double)_model.TimeRemaining / 1440, 1) + "d";
            }
            else if (_model.TimeRemaining > 60)
            {
                timeRemaining = Math.Round((double)_model.TimeRemaining / 60, 1) + "h";
            }
            else
            {
                timeRemaining = _model.TimeRemaining + "m";
            }
        }
    }
}
