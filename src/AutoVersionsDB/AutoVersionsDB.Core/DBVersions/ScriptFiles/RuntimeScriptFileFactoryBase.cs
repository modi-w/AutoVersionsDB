using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class RuntimeScriptFileFactoryBase
    {
        public abstract bool TryParseNextRuntimeScriptFileName(string folderPath, string scriptName, RuntimeScriptFileBase prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile);

        public abstract RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath);
    }

    public abstract class RuntimeScriptFileFactoryBase<TRuntimeScriptFileBase> : RuntimeScriptFileFactoryBase
        where TRuntimeScriptFileBase : RuntimeScriptFileBase
    {

        public override bool TryParseNextRuntimeScriptFileName(string folderPath, string scriptName, RuntimeScriptFileBase prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile)
        {
            return TryParseNextRuntimeScriptFileInstance(folderPath, scriptName, prevRuntimeScriptFile as TRuntimeScriptFileBase, out newRuntimeScriptFile);
        }
        public abstract bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, TRuntimeScriptFileBase prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile);

    }

}
