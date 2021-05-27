using AutoVersionsDB.Helpers;
using System.IO;


namespace AutoVersionsDB.Core
{
    public class AutoVersionsDBSettings
    {
        private readonly string _autoVersionsDBBaseFolder;

        public AutoVersionsDBSettings()
        {
            _autoVersionsDBBaseFolder = FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB");
        }

        public AutoVersionsDBSettings(string autoVersionsDBBaseFolder)
        {
            _autoVersionsDBBaseFolder = FileSystemPathUtils.ParsePathVaribles(autoVersionsDBBaseFolder);
        }


        public string LogFileName => Path.Combine(_autoVersionsDBBaseFolder, "Logs", "AutoVersionsDBLog_{0:yyyy-MM-dd}.txt");

        public string ConfigProjectsFilePath => Path.Combine(_autoVersionsDBBaseFolder, "AutoVersionsDB_ConfigProjects.json");

        public string TempFolderPath => Path.Combine(_autoVersionsDBBaseFolder, "Temp");
    }
}
