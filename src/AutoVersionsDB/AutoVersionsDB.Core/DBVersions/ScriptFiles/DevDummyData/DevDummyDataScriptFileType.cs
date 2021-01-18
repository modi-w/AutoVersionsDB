using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFileType : RepeatableScriptFileType
    {
        public static string DevDummyDataFileTypeCode => ScriptFileTypeBase.Create<DevDummyDataScriptFileType>().FileTypeCode;

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

        public override string FileTypeCode => "DevDummyData";

        public override string Prefix => "dddScript";

    }
}
