using System;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental
{
    public class EmptyDBStateRuntimeScriptFile : IncrementalRuntimeScriptFile
    {
        public const string TargetScriptFileNameEmptyDBState = "--- Empty DB State ---";


        public override string SortKey => "1";


        public override string Filename => TargetScriptFileNameEmptyDBState;


        public EmptyDBStateRuntimeScriptFile()
            : base(null, null, DateTime.MinValue, 0)
        {
        }


    }
}
