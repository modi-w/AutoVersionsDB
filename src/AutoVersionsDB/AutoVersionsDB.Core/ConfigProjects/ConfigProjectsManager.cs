using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public delegate void OnProjectsConfigListChangesHandle();

    public class ConfigProjectsManager
    {
        public List<ProjectConfigItem> ProjectConfigsList { get; private set; }
        public Dictionary<string, ProjectConfigItem> DicProjectConfigItem { get; private set; }

        public ConfigProjectsManager()
        {
            DicProjectConfigItem = new Dictionary<string, ProjectConfigItem>();

            loadAllProjectConfigs();
        }

        public void AddOrUpdateProjectConfig(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            if (string.IsNullOrWhiteSpace(projectConfig.ProjectGuid))
            {
                projectConfig.ProjectGuid = Guid.NewGuid().ToString();
            }

            DicProjectConfigItem[projectConfig.ProjectGuid] = projectConfig;
            saveProjectConfigsFile();
        }

        public void RemoveProjectConfig(string projectGuid)
        {

            DicProjectConfigItem.Remove(projectGuid);

            saveProjectConfigsFile();
        }



        private void saveProjectConfigsFile()
        {
            ProjectConfigsList = DicProjectConfigItem.Values.ToList();
            string projectConfigsListStr = SerializationUtils.JsonSerialize(ProjectConfigsList);

            string configProjectsFilePath = FileSystemPathUtils.ParsePathVaribles(AutoVersionsDBSettings.ConfigProjectsFilePath);
            FileSystemPathUtils.ResloveFilePath(configProjectsFilePath);
            File.WriteAllText(configProjectsFilePath, projectConfigsListStr);
        }


        private void loadAllProjectConfigs()
        {
            DicProjectConfigItem = new Dictionary<string, ProjectConfigItem>();

            string configProjectsFilePath = FileSystemPathUtils.ParsePathVaribles(AutoVersionsDBSettings.ConfigProjectsFilePath);

            if (File.Exists(configProjectsFilePath))
            {
                string projectConfigsListStr = File.ReadAllText(configProjectsFilePath);

                ProjectConfigsList = SerializationUtils.JsonDeserialize<List<ProjectConfigItem>>(projectConfigsListStr);
            }
            else
            {
                ProjectConfigsList = new List<ProjectConfigItem>();
            }

            ProjectConfigsList.OrderBy(e => e.ProjectName).ToList();
            DicProjectConfigItem = ProjectConfigsList.ToDictionary(e => e.ProjectGuid);

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
