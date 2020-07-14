using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ProcessStateBase
    {
        public Dictionary<string, string> EngineMetaData { get; }

        public ExecutionParams ExecutionParams { get; set; }

        public DateTime? StartProcessDateTime { get; set; }
        public DateTime? EndProcessDateTime { get; set; }

        public abstract bool CanRollback { get; }


        public ProcessStateBase()
        {
            EngineMetaData = new Dictionary<string, string>();
        }

        public void SetEngineMetaData(Dictionary<string, string> engineMetaData)
        {
            engineMetaData.ThrowIfNull(nameof(engineMetaData));

            EngineMetaData.Clear();

            foreach (var keyValueParams in engineMetaData)
            {
                EngineMetaData.Add(keyValueParams.Key, keyValueParams.Value);
            }
        }
    }

}
