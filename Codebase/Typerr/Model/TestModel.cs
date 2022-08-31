using System;
using System.Collections.Generic;
using System.Text;

namespace Typerr.Model
{
    public class TestModel
    {
        public Article article { get; set; }
        public TestData testData { get; private set; }

        public TestModel()
        {
            article = new Article();
            testData = new TestData();
        }

        public TestModel(Article article, TestData testData)
        {
            this.article = article;
            this.testData = testData;
        }
    }
}
