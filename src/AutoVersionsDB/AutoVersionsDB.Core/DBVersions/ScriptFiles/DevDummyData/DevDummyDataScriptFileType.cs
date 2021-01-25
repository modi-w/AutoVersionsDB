using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFileType : RepeatableScriptFileType
    {
        public new const string Code = "DevDummyData";
        public override string FileTypeCode => Code;


        public override string Prefix => "dddScript";

    }
}
