using AutoVersionsDB.Common;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFile : RuntimeScriptFileBase
    {
        public override ScriptFileTypeBase ScriptFileType
        {
            get
            {
                return ScriptFileTypeBase.Create<RepeatableScriptFileType>();
            }
        }

        public override string SortKey => ScriptName;
        public override string FolderPath { get; protected set; }

        public override string Filename => $"{ScriptFileType.Prefix}_{ScriptName}.sql";


        protected RepeatableRuntimeScriptFile() { }

        public static RepeatableRuntimeScriptFile CreateByScriptName(string folderPath, string scriptName)
        {
            return new RepeatableRuntimeScriptFile()
            {
                FolderPath = folderPath,
                ScriptName = scriptName
            };
        }

        public RepeatableRuntimeScriptFile(string folderPath, string fileFullPath)
        {
            fileFullPath.ThrowIfNull(nameof(fileFullPath));
            folderPath.ThrowIfNull(nameof(folderPath));

            FolderPath = folderPath;


            FileInfo fiFile = new FileInfo(fileFullPath);

            string shouldBeFileFullPath = Path.Combine(FolderPath, fiFile.Name);

            if (shouldBeFileFullPath.Trim().ToUpperInvariant() != fileFullPath.Trim().ToUpperInvariant())
            {
                throw new ArgumentException($"The argument path: '{fileFullPath}' is different from '{shouldBeFileFullPath}'");
            }

            string filename = fiFile.Name;
            string filenameWithoutExtension = fiFile.Name.Replace(fiFile.Extension, "");


            bool isFilenameValid = Regex.IsMatch(filename, ScriptFileType.RegexFilenamePattern);

            if (!isFilenameValid)
            {
                ScriptFileTypeBase repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();

                string errorMessage = $"Filename '{filename}' not valid for script type: '{repeatableScriptFileType.FileTypeCode}'. Should be like the following pattern: '{ScriptFileType.FilenamePattern}'";
                throw new Exception(errorMessage);
            }

            string[] arrFilenameParts = Regex.Split(filenameWithoutExtension, "_");

            ScriptName = string.Join("_", arrFilenameParts.Skip(1));

        }


    }
}
