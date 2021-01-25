namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableScriptFileType : ScriptFileTypeBase
    {
        public const string Code = "Repeatable";
        public override string FileTypeCode => Code;



        public override string Prefix => "rptScript";


    }
}
