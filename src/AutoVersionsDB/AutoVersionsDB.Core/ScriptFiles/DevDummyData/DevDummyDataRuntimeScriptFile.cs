using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFile : RepeatableRuntimeScriptFile
    {

        public DevDummyDataRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, RepeatableScriptFileProperties repeatableScriptFileProperties)
            : base(scriptFileType, folderPath, repeatableScriptFileProperties)
        {
        }

        public DevDummyDataRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, string fileFullPath)
            : base(scriptFileType, folderPath, fileFullPath)
        {
        }

    }
}
