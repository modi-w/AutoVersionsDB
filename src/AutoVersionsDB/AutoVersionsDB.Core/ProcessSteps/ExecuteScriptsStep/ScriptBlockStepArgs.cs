using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScriptsStep
{
    public class ScriptBlockStepArgs : ActionStepArgs
    {
        public string ScriptBlockStr { get; set; }

        public ScriptBlockStepArgs(string scriptBlockStr)
        {
            ScriptBlockStr = scriptBlockStr;
        }
    }
}
