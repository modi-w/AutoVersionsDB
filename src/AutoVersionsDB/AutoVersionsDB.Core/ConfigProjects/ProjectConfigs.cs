using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public static class ProjectConfigs
    {
       

        public  static void AddOrUpdateProjectConfig(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            if (string.IsNullOrWhiteSpace(projectConfig.ProjectGuid))
            {
                projectConfig.ProjectGuid = Guid.NewGuid().ToString();
            }


            var dicAllProjectConfigs = GetAllProjectConfigs();

            dicAllProjectConfigs[projectConfig.ProjectGuid] = projectConfig;

            SaveProjectConfigsFile(dicAllProjectConfigs);
        }

        public static void RemoveProjectConfig(string projectGuid)
        {
            var dicAllProjectConfigs = GetAllProjectConfigs();

            dicAllProjectConfigs.Remove(projectGuid);

            SaveProjectConfigsFile(dicAllProjectConfigs);
        }



        private static void SaveProjectConfigsFile(Dictionary<string, ProjectConfigItem> dicAllProjectConfigs)
        {
            List<ProjectConfigItem> projectConfigs = dicAllProjectConfigs.Values.ToList();
            string projectConfigsListStr = SerializationUtils.JsonSerialize(projectConfigs);

            string configProjectsFilePath = FileSystemPathUtils.ParsePathVaribles(AutoVersionsDBSettings.ConfigProjectsFilePath);
            FileSystemPathUtils.ResloveFilePath(configProjectsFilePath);
            File.WriteAllText(configProjectsFilePath, projectConfigsListStr);
        }


        public static Dictionary<string, ProjectConfigItem> GetAllProjectConfigs()
        {
            Dictionary<string, ProjectConfigItem> dicAllProjectConfigs = new Dictionary<string, ProjectConfigItem>();

            List<ProjectConfigItem> listAllProjectConfigs;

            string configProjectsFilePath = FileSystemPathUtils.ParsePathVaribles(AutoVersionsDBSettings.ConfigProjectsFilePath);

            if (File.Exists(configProjectsFilePath))
            {
                string projectConfigsListStr = File.ReadAllText(configProjectsFilePath);

                listAllProjectConfigs = SerializationUtils.JsonDeserialize<List<ProjectConfigItem>>(projectConfigsListStr);
            }
            else
            {
                listAllProjectConfigs = new List<ProjectConfigItem>();
            }

            listAllProjectConfigs.OrderBy(e => e.ProjectName).ToList();
            dicAllProjectConfigs = listAllProjectConfigs.ToDictionary(e => e.ProjectGuid);

            return dicAllProjectConfigs;
        }



        //private string clearInvalidCharsForFilename(string inputStr)
        //{
        //    string outStr = inputStr;

        //    if (!string.IsNullOrWhiteSpace(outStr))
        //    {
        //        string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

        //        foreach (char c in invalidChars)
        //        {
        //            outStr = outStr.Replace(c.ToString(), "");
        //        }
        //    }

        //    return outStr;
        //}
    }
}
