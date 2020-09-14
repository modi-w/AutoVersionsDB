using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class RuntimeScriptFileFactoryBase
    {
        public abstract RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, RuntimeScriptFileBase prevRuntimeScriptFile);

        public abstract RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath);
    }

    public abstract class RuntimeScriptFileFactoryBase<TRuntimeScriptFileBase> : RuntimeScriptFileFactoryBase
        where TRuntimeScriptFileBase : RuntimeScriptFileBase
    {

        public override RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, RuntimeScriptFileBase prevRuntimeScriptFile)
        {
            return CreateNextRuntimeScriptFileInstance(folderPath, scriptName, prevRuntimeScriptFile as TRuntimeScriptFileBase);
        }
        public abstract RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, TRuntimeScriptFileBase prevRuntimeScriptFile);

    }

}
