using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class ChooseProjectController
    {
        public IChooseProjectView View { get; }


        public ChooseProjectController(IChooseProjectView view)
        {
            View = view;
        }

      
    }
}
