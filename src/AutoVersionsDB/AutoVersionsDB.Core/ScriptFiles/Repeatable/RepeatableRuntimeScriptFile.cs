using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFile : RuntimeScriptFileBase<RepeatableScriptFileProperties>
    {
        public override string Filename => $"{ScriptFileType.Prefix}_{_scriptFileProperties.ScriptName}.sql";


        public RepeatableRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, RepeatableScriptFileProperties repeatableScriptFileProperties)
            : base(scriptFileType, folderPath, repeatableScriptFileProperties)
        {
        }

        public RepeatableRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, string fileFullPath)
            : base(scriptFileType, folderPath, fileFullPath)
        {
        }

        protected override void parsePropertiesByFileFullPath(string fileFullPath)
        {
            FileInfo fiFile = new FileInfo(fileFullPath);

            string shouldBeFileFullPath = Path.Combine(_folderPath, fiFile.Name);

            if (shouldBeFileFullPath.Trim().ToLower() != fileFullPath.Trim().ToLower())
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

            string scriptName = string.Join("_", arrFilenameParts.Skip(1));

            _scriptFileProperties = new RepeatableScriptFileProperties(scriptName);

        }
    }
}
