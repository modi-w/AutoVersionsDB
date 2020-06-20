using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace AutoVersionsDB.Core.Utils
{
    public static class FileSystemPathUtils
    {

        public static string AssemblyPath
        {
            get
            {
                //Get the assembly information
                Assembly assemblyInfo = Assembly.GetExecutingAssembly();

                //Location is where the assembly is run from
                string assemblyLocation = assemblyInfo.Location;

                //CodeBase is the location of the ClickOnce deployment files
                var uriCodeBase = new Uri(assemblyInfo.CodeBase);
                return Path.GetDirectoryName(uriCodeBase.LocalPath);
            }
        }
        



        public static string RoamingPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }
        public static string LocalAppDataPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            }
        }
        public static string CommonApplicationData
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
        }

        public static string ParsePathVaribles(string path)
        {
            if (path.IndexOf("[AppPath]") > -1)
            {
                return path.Replace("[AppPath]", AssemblyPath);
            }
            else if (path.IndexOf("[Roaming]") > -1)
            {
                return path.Replace("[Roaming]", RoamingPath);
            }
            else if (path.IndexOf("[LocalAppData]") > -1)
            {
                return path.Replace("[LocalAppData]", LocalAppDataPath);
            }
            else if (path.IndexOf("[CommonApplicationData]") > -1)
            {
                return path.Replace("[CommonApplicationData]", CommonApplicationData);
            }
            else
            {
                return path;
            }
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
