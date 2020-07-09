using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public class PrepareEngineEventArgs : EventArgs
    {
        public NotificationableEngineConfig EngineConfig { get; }

        public PrepareEngineEventArgs(NotificationableEngineConfig engineConfig)
        {
            EngineConfig = engineConfig;
        }
    }
}
