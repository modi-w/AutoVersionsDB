namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class EmptyDbStateRuntimeScriptFile : RuntimeScriptFileBase
    {
        public const string TargetScriptFileNameEmptyDBState = "--- Empty DB State ---";

        public override ScriptFilePropertiesBase ScriptFileProperties => null;


        public override string SortKey => "1";

        public override string FileTypeCode => ScriptFileTypeBase.Create<IncrementalScriptFileType>().FileTypeCode;

        public override string Filename => TargetScriptFileNameEmptyDBState;

        public override string FileFullPath => "";

        protected override void ParsePropertiesByFileFullPath(string fileFullPath)
        {

        }
    }
}
