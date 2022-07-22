using System;
using System.Collections.Generic;

namespace Typerr.Model
{
    public class TestData
    {
        private bool _testStarted;

        //TODO: Re-examine this after test pages are implemented
        private int _lastPosition;
        //TODO: Re-examine this after test pages are implemented
        private List<int> _errorPositions;
        
        private List<int> _wpmRates;
        private int _timeLimit;
        private DateTime _startTime;
        private DateTime _endTime;
        private TimeSpan _timeElapsed;
        private TimeSpan _timeRemaining;
        private int _wordsTyped;
        private int _wordsRemaining;
        private int _incorrectWords;


        public TestData(TestData testData = null)
        {
            if (testData != null)
            {
                _testStarted = testData._testStarted;
                _lastPosition = testData._lastPosition;
                _errorPositions = testData._errorPositions;
                _wpmRates = testData._wpmRates;
                _timeLimit = testData._timeLimit;
                _startTime = testData._startTime;
                _endTime = testData._endTime;
                _timeElapsed = testData._timeElapsed;
                _timeRemaining = testData._timeRemaining;
                _wordsTyped = testData._wordsTyped;
                _wordsRemaining = testData._wordsRemaining;
                _incorrectWords = testData._incorrectWords;
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {
            _testStarted = false;
            _lastPosition = 0;
            _errorPositions = new List<int>();
            _wpmRates = new List<int>();
            _timeLimit = 3;
            _startTime = DateTime.MinValue;
            _endTime = DateTime.MaxValue;
            _timeElapsed = TimeSpan.Zero;
            _timeRemaining = TimeSpan.FromMinutes(3);
            _wordsTyped = 0;
            _wordsRemaining = 0;
            _incorrectWords = 0;
        }

    }
}
