using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFileType : RepeatableScriptFileType
    {
        public new const string Code = "DevDummyData";
        public override string FileTypeCode => Code;


        private RuntimeScriptFileFactoryBase _runtimeScriptFileFactory;
        public override RuntimeScriptFileFactoryBase RuntimeScriptFileFactory
        {
            get
            {
                if (_runtimeScriptFileFactory == null)
                {
                    _runtimeScriptFileFactory = new DevDummyDataRuntimeScriptFileFactory();
                }

                return _runtimeScriptFileFactory;
            }
        }


        public override string Prefix => "dddScript";

    }
}
