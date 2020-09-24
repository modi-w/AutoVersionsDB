﻿namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<DevDummyDataRuntimeScriptFile>
    {

        public override bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, DevDummyDataRuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile)
        {
            newRuntimeScriptFile = DevDummyDataRuntimeScriptFile.CreateByScriptName(folderPath, scriptName);

            return newRuntimeScriptFile.IsValidFileName;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new DevDummyDataRuntimeScriptFile(folderPath, fileFullPath);
        }
    }
}
