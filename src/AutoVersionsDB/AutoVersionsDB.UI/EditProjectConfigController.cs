using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class EditProjectConfigController
    {
        public IEditProjectConfigView View { get; }

        public EditProjectConfigController(IEditProjectConfigView view)
        {
            View = view;
        }

        public void SetProjectConfig(string id)
        {

        }

        public void CreateNewProjectConfig()
        {

        }
    }
}
