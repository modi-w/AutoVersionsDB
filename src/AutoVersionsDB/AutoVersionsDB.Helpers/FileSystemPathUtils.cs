using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Helpers
{
    public static class FileSystemPathUtils
    {

        public static string AssemblyPath
        {
            get
            {
                //Get the assembly information
                Assembly assemblyInfo = Assembly.GetExecutingAssembly();

                ////Location is where the assembly is run from
                //string assemblyLocation = assemblyInfo.Location;

                //CodeBase is the location of the ClickOnce deployment files
                var uriCodeBase = new Uri(assemblyInfo.CodeBase);
                return Path.GetDirectoryName(uriCodeBase.LocalPath);
            }
        }




        public static string RoamingPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string LocalAppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string CommonApplicationData => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public static string ParsePathVaribles(string path)
        {
            path.ThrowIfNull(nameof(path));

            if (path.IndexOf("[AppPath]", StringComparison.Ordinal) > -1)
            {
                return path.Replace("[AppPath]", AssemblyPath);
            }
            else if (path.IndexOf("[Roaming]", StringComparison.Ordinal) > -1)
            {
                return path.Replace("[Roaming]", RoamingPath);
            }
            else if (path.IndexOf("[LocalAppData]", StringComparison.Ordinal) > -1)
            {
                return path.Replace("[LocalAppData]", LocalAppDataPath);
            }
            else if (path.IndexOf("[CommonApplicationData]", StringComparison.Ordinal) > -1)
            {
                return path.Replace("[CommonApplicationData]", CommonApplicationData);
            }
            else
            {
                return path;
            }
        }

        public static string GetDllFolderFullPath()
        {
            Assembly assemblyInfo = Assembly.GetExecutingAssembly();
            var uriCodeBase = new Uri(assemblyInfo.CodeBase);
            string dllFolder = Path.GetDirectoryName(uriCodeBase.LocalPath);

            return dllFolder;
        }

        public static void ResloveFilePath(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

    }
}
