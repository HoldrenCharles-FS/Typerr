using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Typerr.Commands;
using Typerr.Components;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class TestViewModel : ViewModelBase
    {
        public TestModel TestModel { get; }

        // Everything the user has typed so far
        private string _userText;
        public string UserText
        {
            get
            {
                return _userText;
            }
            set
            {
                _previousUserText = _userText;
                _userText = value;
                OnPropertyChanged(nameof(UserText));
                if (_testStarted)
                {
                    UpdateTest();
                }
                
                
            }
        }

        private string _previousUserText = "";

        private bool _testStarted = false;

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private RichTextBox _richTextBlock;
        public RichTextBox RichTextBlock
        {
            get
            {
                return _richTextBlock;
            }
            set
            {
                _richTextBlock = value;
                OnPropertyChanged(nameof(RichTextBlock));
            }
        }

        private ObservableCollection<int> _errorPositions;

        public ObservableCollection<int> ErrorPositions
        {
            get { return _errorPositions; }
            set { _errorPositions = value; }
        }

        private List<Run> _runs;

        private readonly SolidColorBrush _gray = new SolidColorBrush(Colors.Gray);
        private readonly SolidColorBrush _lightGray = new SolidColorBrush(Colors.LightGray);

        public TestViewModel(TestModel testModel)
        {
            TestModel = testModel;
            Init();
        }

        private void Init()
        {
            _runs = new List<Run>();
            Text = TestModel.article.text;
            RichTextBlock = new RichTextBox();
            RichTextBlock.BorderThickness = new Thickness(0);
            UserText = "";
            _runs.Add(BuildRun(Text[0].ToString(), RunType.Current));
            _runs.Add(BuildRun(Text[1..], RunType.Untyped));
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(_runs[0]);
            paragraph.Inlines.Add(_runs[1]);
            RichTextBlock.Document.Blocks.Add(paragraph);
            _testStarted = true;
        }

        enum RunType
        {
            Current,
            Right,
            Wrong,
            Untyped
        }

        private Run BuildRun(string text, RunType runType)
        {
            return RunBuilderTask(text, runType);
        }

        private Run BuildRun(char text, RunType runType)
        {
            return RunBuilderTask(text.ToString(), runType);
        }

        private Run RunBuilderTask(string doNotUse, RunType runAway)
        {
            Run run = new Run(doNotUse);
            run.FontSize = 45;
            run.Foreground = new SolidColorBrush(
                (runAway == RunType.Right)
                ? Colors.Black
                : (runAway == RunType.Wrong)
                ? Colors.Red
                : Colors.LightGray);
            run.Background = new SolidColorBrush(
                (runAway == RunType.Current)
                ? Colors.DarkGray
                : Colors.White);

            return run;
        }

        private void UpdateTest()
        {
            // The user entered a new character
            if (_userText.Length == 0)
            {
                _runs.Clear();
                _runs.Add(BuildRun(Text[0].ToString(), RunType.Current));
                _runs.Add(BuildRun(Text[1..], RunType.Untyped));
            }
            else if (_userText.Length == _previousUserText.Length + 1)
            {
                Run untyped = BuildRun(_runs[^1].Text[1..], RunType.Untyped);
                _runs[^1] = BuildRun(_runs[^1].Text[0], RunType.Current);
                _runs.Add(untyped);
                Run lastChar;
                if (_userText[^1] == _text[_userText.Length - 1])
                {
                    lastChar = BuildRun(_userText[^1], RunType.Right);
                }
                else
                {
                    lastChar = BuildRun(Text[_userText.Length - 1], RunType.Wrong);
                }

                _runs[^3] = lastChar;

            }
            // The user hit backspace
            else if (_userText.Length == _previousUserText.Length - 1)
            {
                Run untyped = BuildRun(_text[(_runs.Count - 2)..], RunType.Untyped);
                _runs.RemoveAt(_runs.Count - 1);
                _runs[^1] = untyped;
                _runs[^2] = BuildRun(_text[_runs.Count - 2], RunType.Current);
            }
            RichTextBlock.Document.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            foreach (Run run in _runs)
            {
                paragraph.Inlines.Add(run);
            }
            RichTextBlock.Document.Blocks.Add(paragraph);
        }
    }
}
