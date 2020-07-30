using System;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<IncrementalRuntimeScriptFile>
    {


        public override RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, IncrementalRuntimeScriptFile prevRuntimeScriptFile)
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

            return new IncrementalRuntimeScriptFile(folderPath, scriptName, nextFileDate, nextFileVersion);
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new IncrementalRuntimeScriptFile(folderPath, fileFullPath);
        }

    }
}
