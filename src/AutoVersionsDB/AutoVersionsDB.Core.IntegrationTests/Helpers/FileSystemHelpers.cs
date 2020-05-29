using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests.Helpers
{
    public static class FileSystemHelpers
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
            else if (path.IndexOf("[CommonApplicationData]") > -1)
            {
                return path.Replace("[CommonApplicationData]", CommonApplicationData);
            }
            else
            {
                return path;
            }
        }


        public static string GetEmbeddedResourceFile(string filePathInTheDLL)
        {
            string result;
            //https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = filePathInTheDLL;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static string GetDllFolderFullPath()
        {
            Assembly assemblyInfo = Assembly.GetExecutingAssembly();
            var uriCodeBase = new Uri(assemblyInfo.CodeBase);
            string dllFolder = Path.GetDirectoryName(uriCodeBase.LocalPath);

            return dllFolder;
        }
    }
}
