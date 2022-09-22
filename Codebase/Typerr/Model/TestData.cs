using System;
using System.Collections.Generic;

namespace Typerr.Model
{
    public class TestData
    {
        public bool TestStarted { get; set; }
        public int LastPosition { get;  set; }
        public List<int> ErrorPositions { get; set; }

        public TestData()
        {
            Init();
        }

        private void Init()
        {
            TestStarted = false;
            LastPosition = 0;
            ErrorPositions = new List<int>();
        }

        public void Reset()
        {
            Init();
        }

    }
}
