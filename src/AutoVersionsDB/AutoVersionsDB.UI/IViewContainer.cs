using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public interface IViewContainer
    {
        IView CurrentView { get; }

        void SetView(IView view);
    }
}
