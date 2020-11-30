using System;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<IncrementalRuntimeScriptFile>
    {


        public override bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, IncrementalRuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile)
        {
            int nextFileVersion = 1;
            DateTime nextFileDate = DateTime.Today;

            if (prevRuntimeScriptFile != null)
            {
                if (prevRuntimeScriptFile.Date == nextFileDate)
                {
                    nextFileVersion = prevRuntimeScriptFile.Version + 1;
                }
            }

            newRuntimeScriptFile = new IncrementalRuntimeScriptFile(folderPath, scriptName, nextFileDate, nextFileVersion);

            return newRuntimeScriptFile.IsValidFileName;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new IncrementalRuntimeScriptFile(folderPath, fileFullPath);
        }

    }
}
