using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScriptsStep
{
    public class ScriptFileInfoStepArgs : ActionStepArgs
    {
        public RuntimeScriptFileBase ScriptFile { get; set; }

        public ScriptFileInfoStepArgs(RuntimeScriptFileBase scriptFile)
        {
            ScriptFile = scriptFile;
        }
    }
}
