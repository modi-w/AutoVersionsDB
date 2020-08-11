using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDBSettings
    {
        private static string AutoVersionsDBBaseFolder => FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB");


        public static string LogFileName => Path.Combine(AutoVersionsDBBaseFolder, "Logs", "AutoVersionsDBLog_{0:yyyy-MM-dd}.txt");

        public static string ConfigProjectsFilePath => Path.Combine(AutoVersionsDBBaseFolder, "AutoVersionsDB_ConfigProjects.json");

        public static string TempFolderPath => Path.Combine(AutoVersionsDBBaseFolder, "Temp");
    }
}
