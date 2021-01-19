using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class RuntimeScriptFileBase
    {
        public const string TargetNoneScriptFileName = "#None";
        public const string TargetLastScriptFileName = "#Last";

        public virtual ScriptFileTypeBase ScriptFileType { get; }

        public virtual string SortKey { get; }

        public string ScriptName { get; protected set; }

        public virtual string FolderPath { get; protected set; }
        public virtual string Filename { get; }

        public bool IsValidFileName => Regex.IsMatch(Filename, ScriptFileType.RegexFilenamePattern);

        public string FileFullPath => Path.Combine(FolderPath, Filename);

        public string ComputedHash { get; set; }
        public DateTime ComputedHashDateTime { get; set; }
        public HashDiffType HashDiffType { get; set; }

        public DateTime ExecutedDateTime { get; set; }


        public RuntimeScriptFileBase() { }

        public RuntimeScriptFileBase(string filename)
        {
            Filename = filename;
        }
    }


}
