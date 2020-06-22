using AutoVersionsDB.Core.Utils;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFile : RuntimeScriptFileBase<IncrementalScriptFileProperties>
    {
        public override string Filename => $"{ScriptFileType.Prefix}_{ScriptFilePropertiesInternal.Date.ToString(IncrementalScriptFileType.ScriptFileDatePattern,  CultureInfo.InvariantCulture)}.{ScriptFilePropertiesInternal.Version:000}_{ScriptFilePropertiesInternal.ScriptName}.sql";


        public IncrementalRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, IncrementalScriptFileProperties incrementalScriptFileProperties)
            : base(scriptFileType, folderPath, incrementalScriptFileProperties)
        {
        }

        public IncrementalRuntimeScriptFile(ScriptFileTypeBase scriptFileType, string folderPath, string fileFullPath)
            : base(scriptFileType, folderPath, fileFullPath)
        {
        }


        protected override void ParsePropertiesByFileFullPath(string fileFullPath)
        {
            fileFullPath.ThrowIfNull(nameof(fileFullPath));

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
            if (!DateTime.TryParseExact(currDTFromFilenameStr, IncrementalScriptFileType.ScriptFileDatePattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tempDate_FromFilename))
            {
                string errorMessage = $"Filename not valid date script pattern: '{filename}'. Should be with pattern of '{ IncrementalScriptFileType.ScriptFileDatePattern}'";
                throw new Exception(errorMessage);
            }

            if (tempDate_FromFilename > DateTime.Today)
            {
                string errorMessage = $"Filename not valid date script pattern: '{filename}'. '{tempDate_FromFilename}' Can't be in the future";
                throw new Exception(errorMessage);
            }

            DateTime date = tempDate_FromFilename;


            string currDayVersionFromFilenameStr = arrFileSortStr[1];

            int tempDateVersion_FromFilename;
            if (!int.TryParse(currDayVersionFromFilenameStr, out tempDateVersion_FromFilename))
            {
                string errorMessage = $"Filename not valid for script pattern: '{filename}', the version is not an integer number";
                throw new Exception(errorMessage);
            }
            int version = tempDateVersion_FromFilename;

            ScriptFilePropertiesInternal = new IncrementalScriptFileProperties(scriptName, date, version);

        }
    }
}
