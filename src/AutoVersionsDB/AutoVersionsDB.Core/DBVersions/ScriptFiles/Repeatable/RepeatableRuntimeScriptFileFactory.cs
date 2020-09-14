using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<RepeatableRuntimeScriptFile>
    {

        public override RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string folderPath, string scriptName, RepeatableRuntimeScriptFile prevRuntimeScriptFile)
        {
            return RepeatableRuntimeScriptFile.CreateByScriptName(folderPath, scriptName);
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new RepeatableRuntimeScriptFile(folderPath, fileFullPath);
        }

    }
}
