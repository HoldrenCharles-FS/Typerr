using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Typerr.Model;

namespace Typerr.ViewModel
{
    public class TestViewModel : ViewModelBase
    {
        public TestModel TestModel { get; }

        private readonly User _user;

        public TestPanelViewModel TestPanelVM { get; set; }
        // Everything the user has typed so far
        private string _userText;
        public string UserText
        {
            get => _userText;
            set
            {
                if (!_isPaused)
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
        }

        public int CorrectWords { get; private set; }
        public int CorrectWordsTotal { get; private set; }

        private string _previousUserText = "";

        private bool _testStarted = false;
        private bool _isPaused = false;

        public string Text { get; private set; }

        private RichTextBox _richTextBlock;
        public RichTextBox RichTextBlock
        {
            get => _richTextBlock;
            set
            {
                _richTextBlock = value;
                OnPropertyChanged(nameof(RichTextBlock));
            }
        }

        // List of runs to add to the RichTextBlock
        private List<Run> _runs;
        private Paragraph _paragraph;

        // The list of error positions is needed to know when to undo typing errors
        private List<int> _errorPositions;

        // These variables are related to tracking if the user got a word wrong
        private string[] _words;
        private bool[] _correctWordMap;

        public TestViewModel(TestModel testModel, User user)
        {
            TestModel = testModel;
            _user = user;
            Init();
        }

        private void Init()
        {
            _errorPositions = new List<int>();
            _words = TestModel.article.text.Split(" ");
            _correctWordMap = new bool[_words.Length];
            _correctWordMap[0] = true;
            _runs = new List<Run>();
            Text = TestModel.article.text;
            RichTextBlock = new RichTextBox();
            RichTextBlock.IsReadOnly = true;
            RichTextBlock.IsHitTestVisible = false;
            RichTextBlock.BorderThickness = new Thickness(0);
            UserText = "";
            _runs.Add(BuildRun(Text[0].ToString(), RunType.Current));
            _runs.Add(BuildRun(Text[1..], RunType.Untyped));
            _paragraph = new Paragraph();
            _paragraph.Inlines.Add(_runs[0]);
            _paragraph.Inlines.Add(_runs[1]);
            RichTextBlock.Document.Blocks.Add(_paragraph);
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
                : (doNotUse == " " && runAway == RunType.Wrong)
                ? Color.FromArgb(64, 255, 0, 0)
                : Colors.White);

            return run;
        }

        private void UpdateTest()
        {
            TextPointer textPointer = _runs[^2].ContentStart;

            var characterRect = textPointer.GetCharacterRect(LogicalDirection.Forward);
            RichTextBlock.ScrollToVerticalOffset(RichTextBlock.VerticalOffset + characterRect.Top - RichTextBlock.ActualHeight / 2d);


            // The user hit backspace to the beginning
            if (_userText.Length == 0)
            {
                RichTextBlock.Document.Blocks.Clear();
                _paragraph.Inlines.Clear();
                _runs.Clear();
                _runs.Add(BuildRun(Text[0].ToString(), RunType.Current));
                _runs.Add(BuildRun(Text[1..], RunType.Untyped));
                _paragraph.Inlines.Add(_runs[0]);
                _paragraph.Inlines.Add(_runs[1]);
                RichTextBlock.Document.Blocks.Add(_paragraph);

                TestPanelVM.TypingErrors = 0;
                TestPanelVM.WordsTyped = 0;
                if (_user.Mode == 1)
                {
                    TestPanelVM.ModeData = TestModel.WordCount.ToString();
                }
            }
            // The user has hit the end
            else if (_userText.Length == Text.Length)
            {
                _paragraph.Inlines.Remove(_runs[^1]);
                _runs.RemoveAt(_runs.Count - 1);

                if (_userText[^1] == Text[_userText.Length - 1])
                {
                    Run right = BuildRun(_userText[^1], RunType.Right);

                    _paragraph.Inlines.InsertAfter(_runs[^1], right);
                    _paragraph.Inlines.Remove(_runs[^1]);

                    _runs[^1] = right;

                    if (_correctWordMap[TestPanelVM.WordsTyped])
                    {
                        CorrectWords++;
                        CorrectWordsTotal++;
                    }
                }
                else
                {
                    Run wrong = BuildRun(Text[_userText.Length - 1], RunType.Wrong);

                    _paragraph.Inlines.InsertAfter(_runs[^1], wrong);
                    _paragraph.Inlines.Remove(_runs[^1]);

                    _runs[^1] = wrong;
                    _errorPositions.Add(_userText.Length - 1);
                    TestPanelVM.TypingErrors++;

                    _correctWordMap[TestPanelVM.WordsTyped] = false;
                    
                }
                TestPanelVM.WordsTyped++;
                _isPaused = true;
                TestPanelVM.StopTest();
            }
            // The user entered a new character
            else if (_userText.Length == _previousUserText.Length + 1)
            {
                // Modify the list of runs

                // Define new runs
                Run untyped = BuildRun(_runs[^1].Text[1..], RunType.Untyped);
                Run current = BuildRun(_runs[^1].Text[0], RunType.Current);

                // Replace the old untyped in the paragraph with a current
                _paragraph.Inlines.Remove(_runs[^1]);
                _paragraph.Inlines.Add(current);

                // Replace the old untyped in the runs with a current char
                _runs[^1] = current;

                // Add the new untyped to the paragraph and runs
                _paragraph.Inlines.Add(untyped);
                _runs.Add(untyped);

                // Define the previous char
                Run previousChar;
                if (_userText[^1] == Text[_userText.Length - 1])
                {
                    previousChar = BuildRun(_userText[^1], RunType.Right);
                }
                else
                {
                    previousChar = BuildRun(Text[_userText.Length - 1], RunType.Wrong);
                    _errorPositions.Add(_userText.Length - 1);
                    TestPanelVM.TypingErrors++;

                    if (Text[_userText.Length - 1] != ' ')
                    {
                        _correctWordMap[TestPanelVM.WordsTyped] = false;
                    }
                }

                // Replace the old current with the previous char
                _paragraph.Inlines.InsertAfter(_runs[^3], previousChar);
                _paragraph.Inlines.Remove(_runs[^3]);
                _runs[^3] = previousChar;

                // Word Count
                if (Text[_userText.Length - 1] == ' ')
                {
                    TestPanelVM.WordsTyped++;

                    if (_correctWordMap[TestPanelVM.WordsTyped - 1])
                    {
                        CorrectWords++;
                        CorrectWordsTotal++;
                    }

                    if (_user.Mode == 1)
                    {
                        TestPanelVM.ModeData = (int.Parse(TestPanelVM.ModeData) - 1).ToString();
                    }

                    // Reset the flag for the next word
                    _correctWordMap[TestPanelVM.WordsTyped] = true;
                }
            }
            // The user hit backspace
            else if (_userText.Length == _previousUserText.Length - 1)
            {
                // Define new runs
                Run untyped = BuildRun(Text[(_runs.Count - 2)..], RunType.Untyped);
                Run current = BuildRun(Text[_runs.Count - 3], RunType.Current);

                // Replace the old current with the new untyped
                _paragraph.Inlines.Remove(_runs[^1]);
                _paragraph.Inlines.InsertAfter(_runs[^2], untyped);
                _paragraph.Inlines.Remove(_runs[^2]);

                _runs.RemoveAt(_runs.Count - 1);
                _runs[^1] = untyped;

                // Replace the previous char with a current
                _paragraph.Inlines.InsertAfter(_runs[^2], current);
                _paragraph.Inlines.Remove(_runs[^2]);
                _runs[^2] = current;

                // Typing errors
                if (_errorPositions.Count > 0 && _errorPositions[^1] == _userText.Length)
                {
                    _errorPositions.RemoveAt(_errorPositions.Count - 1);
                    TestPanelVM.TypingErrors--;
                }

                // Word Count
                if (Text[_userText.Length - 1] == ' ')
                {
                    _correctWordMap[TestPanelVM.WordsTyped] = false;
                    TestPanelVM.WordsTyped--;

                    if (CorrectWords > 0)
                    {
                        CorrectWords--;
                        CorrectWordsTotal--;
                    }

                    if (_user.Mode == 1)
                    {
                        TestPanelVM.ModeData = (int.Parse(TestPanelVM.ModeData) + 1).ToString();
                    }

                }
            }
        }

        internal void Unpause()
        {
            _isPaused = false;
        }

        internal void Pause()
        {
            _isPaused = true;
        }

        public void ResetCorrectWords()
        {
            CorrectWords = 0;
        }

        internal void Restart()
        {
            _isPaused = false;
            _testStarted = false;
            _previousUserText = "";
            CorrectWords = 0;
            CorrectWordsTotal = 0;
            Init();
        }
    }
}
