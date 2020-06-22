using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase
    {


        public override RuntimeScriptFileBase CreateNextNewScriptFileInstance(ScriptFilePropertiesBase lastExecutedFileProperties, string folderPath, string scriptName)
        {
            RepeatableScriptFileProperties scriptFileProperties = new RepeatableScriptFileProperties(scriptName);
            RuntimeScriptFileBase newRuntimeScriptFile = new RepeatableRuntimeScriptFile(ScriptFileTypeBase.Create<RepeatableScriptFileType>(), folderPath, scriptFileProperties);

            return newRuntimeScriptFile;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new RepeatableRuntimeScriptFile(ScriptFileTypeBase.Create<RepeatableScriptFileType>(),folderPath, fileFullPath);
        }

    }
}
