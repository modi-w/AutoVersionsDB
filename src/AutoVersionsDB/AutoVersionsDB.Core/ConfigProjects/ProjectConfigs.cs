using AutoVersionsDB.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoVersionsDB.Core.ConfigProjects
{

    public class ProjectConfigs
    {

        private static object _saveSync = new object();


        public void SaveNewProjectConfig(ProjectConfigItem projectConfig)
        {
            lock (_saveSync)
            {
                projectConfig.ThrowIfNull(nameof(projectConfig));
                projectConfig.ProjectCode.ThrowIfNull(nameof(projectConfig.ProjectCode));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (dicAllProjectConfigs.ContainsKey(projectConfig.ProjectCode))
                {
                    throw new Exception($"ProjectCode: '{projectConfig.ProjectCode}' is aready exist.");
                }

                dicAllProjectConfigs.Add(projectConfig.ProjectCode, projectConfig);

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }

        }

        public void UpdateProjectConfig(ProjectConfigItem projectConfig)
        {
            lock (_saveSync)
            {
                projectConfig.ThrowIfNull(nameof(projectConfig));
                projectConfig.ProjectCode.ThrowIfNull(nameof(projectConfig.ProjectCode));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.ContainsKey(projectConfig.ProjectCode))
                {
                    throw new Exception($"ProjectCode: '{projectConfig.ProjectCode}' is not exist.");
                }


                dicAllProjectConfigs[projectConfig.ProjectCode] = projectConfig;

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }
        }

        public void ChangeProjectCode(string prevProjectCode, string newProjectCode)
        {
            lock (_saveSync)
            {
                prevProjectCode.ThrowIfNull(nameof(prevProjectCode));
                newProjectCode.ThrowIfNull(nameof(newProjectCode));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.TryGetValue(prevProjectCode, out ProjectConfigItem projectConfig))
                {
                    throw new Exception($"ProjectCode: '{projectConfig.ProjectCode}' is not exist.");
                }

                projectConfig.ProjectCode = newProjectCode;

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }
        }

        public void RemoveProjectConfig(string projectCode)
        {
            lock (_saveSync)
            {
                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.ContainsKey(projectCode))
                {
                    throw new Exception($"ProjectCode: '{projectCode}' is not exist.");
                }


                dicAllProjectConfigs.Remove(projectCode);

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }
        }



        private void SaveProjectConfigsFile(Dictionary<string, ProjectConfigItem> dicAllProjectConfigs)
        {
            List<ProjectConfigItem> projectConfigs = dicAllProjectConfigs.Values.ToList();
            string projectConfigsListStr = SerializationUtils.JsonSerialize(projectConfigs);

            string configProjectsFilePath = FileSystemPathUtils.ParsePathVaribles(AutoVersionsDBSettings.ConfigProjectsFilePath);
            FileSystemPathUtils.ResloveFilePath(configProjectsFilePath);
            File.WriteAllText(configProjectsFilePath, projectConfigsListStr);
        }


        public Dictionary<string, ProjectConfigItem> GetAllProjectConfigs()
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

            listAllProjectConfigs.OrderBy(e => e.ProjectCode).ToList();
            dicAllProjectConfigs = listAllProjectConfigs.ToDictionary(e => e.ProjectCode);

            return dicAllProjectConfigs;
        }

        public virtual ProjectConfigItem GetProjectConfigByProjectCode(string projectCode)
        {
            var dicAllProjectConfigs = GetAllProjectConfigs();

            return dicAllProjectConfigs[projectCode];
        }

        public virtual bool IsProjectCodeExsit(string projectCode)
        {
            var dicAllProjectConfigs = GetAllProjectConfigs();

            return dicAllProjectConfigs.ContainsKey(projectCode);
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
