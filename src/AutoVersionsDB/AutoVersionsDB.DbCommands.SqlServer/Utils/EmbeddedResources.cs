using System.IO;
using System.Reflection;

namespace AutoVersionsDB.DbCommands.SqlServer.Utils
{
    public static class EmbeddedResources
    {
        public static string GetEmbeddedResourceFile(string filePathInTheDLL)
        {
            string result;
            //https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = filePathInTheDLL;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }


            return result;
        }
    }
}
