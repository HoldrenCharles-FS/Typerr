using System;
using System.Collections.Generic;
using System.Text;

namespace Typerr.Model
{
    public class HomeModel
    {
        public List<TestModel> Library { get; private set; }

        public HomeModel(List<TestModel> libTileModels = null)
        {
            //TODO: Update this
            Library = new List<TestModel>();
        }
    }
}
