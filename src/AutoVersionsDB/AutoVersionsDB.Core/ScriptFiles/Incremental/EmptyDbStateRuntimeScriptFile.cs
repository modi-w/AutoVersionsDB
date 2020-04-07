namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class EmptyDbStateRuntimeScriptFile : RuntimeScriptFileBase
    {
        public const string C_TargetScriptFileName_EmptyDBState = "--- Empty DB State ---";

        public override ScriptFilePropertiesBase ScriptFileProperties => null;


        public override string SortKey => "1";

        public override string FileTypeCode => ScriptFileTypeBase.Create<IncrementalScriptFileType>().FileTypeCode;

        public override string Filename => C_TargetScriptFileName_EmptyDBState;

        public override string FileFullPath => "";

        protected override void parsePropertiesByFileFullPath(string fileFullPath)
        {

        }
    }
}
