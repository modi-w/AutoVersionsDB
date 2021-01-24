//namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
//{
//    public abstract class RuntimeScriptFileFactoryBase
//    {
//        public abstract bool TryParseNextRuntimeScriptFileName(string folderPath, string scriptName, RuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile);

//        public abstract RuntimeScriptFile CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath);
//    }

//    public abstract class RuntimeScriptFileFactoryBase<TRuntimeScriptFileBase> : RuntimeScriptFileFactoryBase
//        where TRuntimeScriptFileBase : RuntimeScriptFile
//    {

//        public override bool TryParseNextRuntimeScriptFileName(string folderPath, string scriptName, RuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile)
//        {
//            return TryParseNextRuntimeScriptFileInstance(folderPath, scriptName, prevRuntimeScriptFile as TRuntimeScriptFileBase, out newRuntimeScriptFile);
//        }
//        public abstract bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, TRuntimeScriptFileBase prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile);

//    }

//}
