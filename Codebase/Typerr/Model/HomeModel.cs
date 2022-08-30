using System;
using System.Collections.Generic;
using System.Text;

namespace Typerr.Model
{
    public class HomeModel
    {
        public List<LibTileModel> Library { get; private set; }

        public HomeModel(List<LibTileModel> libTileModels = null)
        {
            //TODO: Update this
            Library = new List<LibTileModel>();
        }
    }
}
