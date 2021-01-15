using AutoVersionsDB.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFile : RuntimeScriptFileBase
    {
        public override ScriptFileTypeBase ScriptFileType => ScriptFileTypeBase.Create<RepeatableScriptFileType>();

        public override string SortKey => $"{OrderNum:000}{ScriptName}";
        public override string FolderPath { get; protected set; }

        public override string Filename => $"{ScriptFileType.Prefix}_{OrderNum:000}_{ScriptName}.sql";

        public int OrderNum { get; set; }

        protected RepeatableRuntimeScriptFile() { }

        public static RepeatableRuntimeScriptFile CreateByScriptName(string folderPath, string scriptName, int orderNum)
        {
            return new RepeatableRuntimeScriptFile()
            {
                FolderPath = folderPath,
                ScriptName = scriptName,
                OrderNum = orderNum,
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

            ScriptName = string.Join("_", arrFilenameParts.Skip(2));

            string fileOrderNumStr = arrFilenameParts[1];

            if (!int.TryParse(fileOrderNumStr, out int tempOrderNum_FromFilename))
            {
                string errorMessage = $"Filename not valid for script pattern: '{filename}', the 'OrderNum' is not an integer number";
                throw new Exception(errorMessage);
            }

            OrderNum = tempOrderNum_FromFilename;
        }


    }
}
