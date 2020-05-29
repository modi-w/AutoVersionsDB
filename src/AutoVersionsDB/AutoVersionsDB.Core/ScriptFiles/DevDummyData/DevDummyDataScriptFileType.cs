using AutoVersionsDB.Core.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFileType : RepeatableScriptFileType
    {
        public override string FileTypeCode => "DevDummyData";

        public override string Prefix => "dddScript";

    }
}
