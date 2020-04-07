using System;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFile_Factory : RuntimeScriptFile_FactoryBase
    {
        public IncrementalRuntimeScriptFile_Factory()
        {
        }

        public override RuntimeScriptFileBase CreateNextNewScriptFileInstance(ScriptFilePropertiesBase lastExecutedFileProperties, string folderPath, string scriptName)
        {
            int nextFileVersion = 1;

            IncrementalScriptFileProperties incrementalScriptFileProperties = lastExecutedFileProperties as IncrementalScriptFileProperties;


            if (incrementalScriptFileProperties != null
                && incrementalScriptFileProperties.Date == DateTime.Today)
            {
                nextFileVersion = incrementalScriptFileProperties.Version + 1;
            }

            IncrementalScriptFileProperties scriptFileProperties = new IncrementalScriptFileProperties(scriptName, DateTime.Today, nextFileVersion);
            RuntimeScriptFileBase newRuntimeScriptFile = new IncrementalRuntimeScriptFile(ScriptFileTypeBase.Create<IncrementalScriptFileType>(), folderPath, scriptFileProperties);

            return newRuntimeScriptFile;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new IncrementalRuntimeScriptFile(ScriptFileTypeBase.Create<IncrementalScriptFileType>(), folderPath, fileFullPath);
        }

    }
}
