using System;
using System.Collections.Generic;

namespace Typerr.Model
{
    public class TestData
    {
        public bool TestStarted { get; set; }

        //TODO: Re-examine this after test pages are implemented
        public int LastPosition { get;  set; }
        //TODO: Re-examine this after test pages are implemented
        public List<int> ErrorPositions { get; private set; }

        public List<int> WpmRates { get; private set; }
        public int TimeLimit { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan TimeElapsed { get; private set; }
        public TimeSpan TimeRemaining { get; private set; }
        public int WordsTyped { get; private set; }
        public int WordsRemaining { get; private set; }
        public int IncorrectWords { get; private set; }

        public TestData(TestData testData = null)
        {
            if (testData != null)
            {
                TestStarted = testData.TestStarted;
                LastPosition = testData.LastPosition;
                ErrorPositions = testData.ErrorPositions;
                WpmRates = testData.WpmRates;
                TimeLimit = testData.TimeLimit;
                StartTime = testData.StartTime;
                EndTime = testData.EndTime;
                TimeElapsed = testData.TimeElapsed;
                TimeRemaining = testData.TimeRemaining;
                WordsTyped = testData.WordsTyped;
                WordsRemaining = testData.WordsRemaining;
                IncorrectWords = testData.IncorrectWords;
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {
            TestStarted = false;
            LastPosition = 0;
            ErrorPositions = new List<int>();
            WpmRates = new List<int>();
            TimeLimit = 3;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            TimeElapsed = TimeSpan.Zero;
            TimeRemaining = TimeSpan.FromMinutes(3);
            WordsTyped = 0;
            WordsRemaining = 0;
            IncorrectWords = 0;
        }

    }
}
