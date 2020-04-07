using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace AutoVersionsDB.Core
{
    public class AutoVersionsDBSettings
    {
        private static string _autoVersionsDBBaseFolder => FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB");
       // private static string _settingsFilePath => Path.Combine(_autoVersionsDBBaseFolder, "AutoVersionsDB_Setting.txt");


        public static string LogFileName => Path.Combine(_autoVersionsDBBaseFolder, "Logs", "AutoVersionsDBLog_{0:yyyy-MM-dd}.txt");

        public static string ConfigProjectsFilePath => Path.Combine(_autoVersionsDBBaseFolder, "AutoVersionsDB_ConfigProjects.json");

        public static string TempFolderPath => Path.Combine(_autoVersionsDBBaseFolder, "Temp");
    }
}
