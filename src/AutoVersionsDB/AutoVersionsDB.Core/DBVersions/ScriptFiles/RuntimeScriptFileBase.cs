using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class RuntimeScriptFileBase
    {
        public const string TargetNoneScriptFileName = "#None";
        public const string TargetLastScriptFileName = "#Last";

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
