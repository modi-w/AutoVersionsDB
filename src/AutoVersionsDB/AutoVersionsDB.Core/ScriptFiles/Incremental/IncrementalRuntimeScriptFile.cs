using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFile : RuntimeScriptFileBase<IncrementalScriptFileProperties>
    {
        public const string C_ScriptFile_DatePattern = "yyyy-MM-dd";

        public override string Filename => $"{ScriptFileType.Prefix}_{_scriptFileProperties.Date.ToString(IncrementalScriptFileProperties.C_ScriptFile_DatePattern)}.{_scriptFileProperties.Version.ToString("000")}_{_scriptFileProperties.ScriptName}.sql";


        public IncrementalRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, IncrementalScriptFileProperties incrementalScriptFileProperties)
            : base(scriptFileType, folderPath, incrementalScriptFileProperties)
        {
        }

        public IncrementalRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, string fileFullPath)
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
                ScriptFileTypeBase incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();

                string errorMessage = $"Filename '{filename}' not valid for script type: '{incrementalScriptFileType.FileTypeCode}'. Should be like the following pattern: '{ScriptFileType.FilenamePattern}'";
                throw new Exception(errorMessage);
            }

            string[] arrFilenameParts = Regex.Split(filenameWithoutExtension, "_");

            string scriptName = string.Join("_", arrFilenameParts.Skip(2));

            string fileSortStr = arrFilenameParts[1];

            string[] arrFileSortStr = Regex.Split(fileSortStr, @"\.", RegexOptions.IgnorePatternWhitespace);


            string currDTFromFilenameStr = arrFileSortStr[0];

            DateTime tempDate_FromFilename;
            if (!DateTime.TryParseExact(currDTFromFilenameStr, C_ScriptFile_DatePattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tempDate_FromFilename))
            {
                string errorMessage = string.Format("Filename not valid date script pattern: '{0}'. Should be with pattern of '{1}'", filename, C_ScriptFile_DatePattern);
                throw new Exception(errorMessage);
            }

            if (tempDate_FromFilename > DateTime.Today)
            {
                string errorMessage = string.Format("Filename not valid date script pattern: '{0}'. '{1}' Can't be in the future", filename, tempDate_FromFilename);
                throw new Exception(errorMessage);
            }

            DateTime date = tempDate_FromFilename;


            string currDayVersionFromFilenameStr = arrFileSortStr[1];

            int tempDateVersion_FromFilename;
            if (!int.TryParse(currDayVersionFromFilenameStr, out tempDateVersion_FromFilename))
            {
                string errorMessage = string.Format("Filename not valid for script pattern: '{0}', the version is not an integer number", filename);
                throw new Exception(errorMessage);
            }
            int version = tempDateVersion_FromFilename;

            _scriptFileProperties = new IncrementalScriptFileProperties(scriptName, date, version);

        }
    }
}
