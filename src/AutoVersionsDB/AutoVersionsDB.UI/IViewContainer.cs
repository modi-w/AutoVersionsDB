using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public interface IViewContainer
    {
        ViewType CurrentView { get; }
        void SetView(ViewType viewType);
    }
}
