using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class RuntimeScriptFileBase
    {
        public abstract ScriptFileTypeBase ScriptFileType { get; }

        public abstract string SortKey { get; }

        public string ScriptName { get; protected set; }

        public abstract string FolderPath { get; protected set; }
        public abstract string Filename { get; }
        public bool IsValidFileName => Regex.IsMatch(Filename, ScriptFileType.RegexFilenamePattern);

        public string FileFullPath => Path.Combine(FolderPath, Filename);

        public string ComputedHash { get; set; }
        public DateTime ComputedHashDateTime { get; set; }
        public HashDiffType HashDiffType { get; set; }

        public DateTime ExecutedDateTime { get; set; }



    }


}
