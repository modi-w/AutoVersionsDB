namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class EmptyDbStateRuntimeScriptFile : IncrementalRuntimeScriptFile
    {
        public const string TargetScriptFileNameEmptyDBState = "--- Empty DB State ---";


        public override string SortKey => "1";


        public override string Filename => TargetScriptFileNameEmptyDBState;


        public EmptyDbStateRuntimeScriptFile()
            :base(null,null)
        {
        }


    }
}
