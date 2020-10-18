using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public interface IMainView : IView
    {
        bool BtnChooseProjectVisible { get; set; }

    }
}
