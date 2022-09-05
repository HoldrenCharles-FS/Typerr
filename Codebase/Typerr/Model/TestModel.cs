using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Typerr.Model
{
    public class TestModel
    {
        public Article article { get; set; }
        public TestData testData { get; private set; }

        // Additional Fields
        private readonly DateTime _creationDate;
        public BitmapImage Image { get; set; }
        public BitmapImage Favicon { get; set; }
        public int WordCount { get; set; }

        public TestModel()
        {
            article = new Article();
            testData = new TestData();
            _creationDate = DateTime.Now;
        }
       
    }
}
