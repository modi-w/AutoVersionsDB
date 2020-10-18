using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class MainController
    {
        public IMainView View { get; }


        public MainController(IMainView view)
        {
            View = view;
        }

      
    }
}
