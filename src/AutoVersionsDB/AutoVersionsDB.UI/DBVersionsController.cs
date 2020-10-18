using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class DBVersionsController
    {
        public IDBVersionsView View { get; }

        public DBVersionsController(IDBVersionsView view)
        {
            View = view;
        }

        public void SetProjectConfig(string id)
        {

        }

    }
}
