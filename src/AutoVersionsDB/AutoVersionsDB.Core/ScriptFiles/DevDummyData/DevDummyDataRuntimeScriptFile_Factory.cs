
namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFile_Factory : RuntimeScriptFile_FactoryBase
    {

        public override RuntimeScriptFileBase CreateNextNewScriptFileInstance(ScriptFilePropertiesBase lastExecutedFileProperties, string folderPath, string scriptName)
        {
            DevDummyDataScriptFileProperties scriptFileProperties = new DevDummyDataScriptFileProperties(scriptName);
            RuntimeScriptFileBase newRuntimeScriptFile = new DevDummyDataRuntimeScriptFile(ScriptFileTypeBase.Create<DevDummyDataScriptFileType>(), folderPath, scriptFileProperties);

            return newRuntimeScriptFile;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new DevDummyDataRuntimeScriptFile(ScriptFileTypeBase.Create<DevDummyDataScriptFileType>(), folderPath, fileFullPath);
        }

    }
}
