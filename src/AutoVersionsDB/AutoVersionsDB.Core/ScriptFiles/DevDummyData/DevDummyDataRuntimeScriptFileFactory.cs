
namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<DevDummyDataRuntimeScriptFile>
    {

        public override RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, DevDummyDataRuntimeScriptFile prevRuntimeScriptFile)
        {
            return DevDummyDataRuntimeScriptFile.CreateByScriptName(folderPath, scriptName);
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new DevDummyDataRuntimeScriptFile(folderPath, fileFullPath);
        }
    }
}
