using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class RuntimeScriptFileFactoryBase
    {
        public abstract RuntimeScriptFileBase CreateNextNewScriptFileInstance(ScriptFilePropertiesBase lastExecutedFileProperties,string folderPath,string scriptName);

        public abstract RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath);

    }
}
