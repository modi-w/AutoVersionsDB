using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFile : RepeatableRuntimeScriptFile
    {
        public override ScriptFileTypeBase ScriptFileType
        {
            get
            {
                return ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
            }
        }
        protected DevDummyDataRuntimeScriptFile() { }

        public static new DevDummyDataRuntimeScriptFile CreateByScriptName(string folderPath, string scriptName)
        {
            return new DevDummyDataRuntimeScriptFile()
            {
                FolderPath = folderPath,
                ScriptName = scriptName
            };
        }

        public DevDummyDataRuntimeScriptFile(string folderPath, string fileFullPath)
          : base(folderPath, fileFullPath)
        {
        }



    }
}
