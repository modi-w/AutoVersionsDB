using AutoVersionsDB.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental
{
    public class IncrementalRuntimeScriptFile : RuntimeScriptFileBase
    {
        public override ScriptFileTypeBase ScriptFileType
        {
            get
            {
                return ScriptFileTypeBase.Create<IncrementalScriptFileType>();
            }
        }

        public override string SortKey => $"{Date.ToString(IncrementalScriptFileType.ScriptFileDatePattern, CultureInfo.InvariantCulture)}{Version:000}{ScriptName}";

        public override string FolderPath { get; protected set; }
        public override string Filename => $"{ScriptFileType.Prefix}_{Date.ToString(IncrementalScriptFileType.ScriptFileDatePattern, CultureInfo.InvariantCulture)}.{Version:000}_{ScriptName}.sql";

        public DateTime Date { get; set; }
        public int Version { get; set; }




        public IncrementalRuntimeScriptFile(string folderPath, string scriptName, DateTime date, int version)
        {
            FolderPath = folderPath;
            ScriptName = scriptName;
            Date = date;
            Version = version;

           
        }

        public IncrementalRuntimeScriptFile(string folderPath, string fileFullPath)
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
                ScriptFileTypeBase incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();

                string errorMessage = $"Filename '{filename}' not valid for script type: '{incrementalScriptFileType.FileTypeCode}'. Should be like the following pattern: '{ScriptFileType.FilenamePattern}'";
                throw new Exception(errorMessage);
            }

            string[] arrFilenameParts = Regex.Split(filenameWithoutExtension, "_");

            ScriptName = string.Join("_", arrFilenameParts.Skip(2));

            string fileSortStr = arrFilenameParts[1];

            string[] arrFileSortStr = Regex.Split(fileSortStr, @"\.", RegexOptions.IgnorePatternWhitespace);


            string currDTFromFilenameStr = arrFileSortStr[0];

            if (!DateTime.TryParseExact(currDTFromFilenameStr, IncrementalScriptFileType.ScriptFileDatePattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate_FromFilename))
            {
                string errorMessage = $"Filename not valid date script pattern: '{filename}'. Should be with pattern of '{ IncrementalScriptFileType.ScriptFileDatePattern}'";
                throw new Exception(errorMessage);
            }

            if (tempDate_FromFilename > DateTime.Today)
            {
                string errorMessage = $"Filename not valid date script pattern: '{filename}'. '{tempDate_FromFilename}' Can't be in the future";
                throw new Exception(errorMessage);
            }

            Date = tempDate_FromFilename;


            string currDayVersionFromFilenameStr = arrFileSortStr[1];

            if (!int.TryParse(currDayVersionFromFilenameStr, out int tempDateVersion_FromFilename))
            {
                string errorMessage = $"Filename not valid for script pattern: '{filename}', the version is not an integer number";
                throw new Exception(errorMessage);
            }

            Version = tempDateVersion_FromFilename;
        }






    }
}
