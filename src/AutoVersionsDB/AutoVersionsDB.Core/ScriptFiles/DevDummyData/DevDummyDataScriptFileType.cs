using AutoVersionsDB.Core.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFileType : RepeatableScriptFileType
    {
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
