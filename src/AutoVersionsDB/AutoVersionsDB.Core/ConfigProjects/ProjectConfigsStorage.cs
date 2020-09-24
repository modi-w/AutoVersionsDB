using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.Core.ConfigProjects
{

    public class ProjectConfigsStorage
    {

        private static object _saveSync = new object();


        public void SaveNewProjectConfig(ProjectConfigItem projectConfig)
        {
            lock (_saveSync)
            {
                projectConfig.ThrowIfNull(nameof(projectConfig));
                projectConfig.Id.ThrowIfNull(nameof(projectConfig.Id));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (dicAllProjectConfigs.ContainsKey(projectConfig.Id.ToTrimedInvariant()))
                {
                    throw new Exception($"Id: '{projectConfig.Id}' is aready exist.");
                }

                dicAllProjectConfigs.Add(projectConfig.Id.ToTrimedInvariant(), projectConfig);

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }

        }

        public void UpdateProjectConfig(ProjectConfigItem projectConfig)
        {
            lock (_saveSync)
            {
                projectConfig.ThrowIfNull(nameof(projectConfig));
                projectConfig.Id.ThrowIfNull(nameof(projectConfig.Id));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.ContainsKey(projectConfig.Id.ToTrimedInvariant()))
                {
                    throw new Exception($"Id: '{projectConfig.Id}' is not exist.");
                }


                dicAllProjectConfigs[projectConfig.Id.ToTrimedInvariant()] = projectConfig;

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }
        }

        public void ChangeId(string prevId, string newId)
        {
            lock (_saveSync)
            {
                prevId.ThrowIfNull(nameof(prevId));
                newId.ThrowIfNull(nameof(newId));

                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.TryGetValue(prevId.ToTrimedInvariant(), out ProjectConfigItem projectConfig))
                {
                    throw new Exception($"Id: '{projectConfig.Id}' is not exist.");
                }

                projectConfig.Id = newId;

                SaveProjectConfigsFile(dicAllProjectConfigs);
            }
        }

        public void RemoveProjectConfig(string id)
        {
            lock (_saveSync)
            {
                var dicAllProjectConfigs = GetAllProjectConfigs();

                if (!dicAllProjectConfigs.ContainsKey(id.ToTrimedInvariant()))
                {
                    throw new Exception($"Id: '{id}' is not exist.");
                }


                dicAllProjectConfigs.Remove(id.ToTrimedInvariant());

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

            listAllProjectConfigs.OrderBy(e => e.Id.ToTrimedInvariant()).ToList();
            dicAllProjectConfigs = listAllProjectConfigs.ToDictionary(e => e.Id.ToTrimedInvariant());

            return dicAllProjectConfigs;
        }

        public virtual ProjectConfigItem GetProjectConfigById(string id)
        {
            var dicAllProjectConfigs = GetAllProjectConfigs();

            dicAllProjectConfigs.TryGetValue( id.ToTrimedInvariant(), out ProjectConfigItem resultProjectConfig);

            return resultProjectConfig;
        }

        public virtual bool IsIdExsit(string id)
        {
            var dicAllProjectConfigs = GetAllProjectConfigs();

            return dicAllProjectConfigs.ContainsKey(id.ToTrimedInvariant());
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
