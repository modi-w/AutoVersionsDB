using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{

    public static class ProjectConfigsFactory
    {
        private static List<DBConnectionInfo> _dbConnectionInfos = new List<DBConnectionInfo>()
        {
            new DBConnectionInfo("SqlServer",
                                SqlServerInstanceHelpers.ConnectionStringBuilder.DataSource,
                                "AutoVersionsDB.Tests",
                                SqlServerInstanceHelpers.ConnectionStringBuilder.UserID,
                                SqlServerInstanceHelpers.ConnectionStringBuilder.Password)
        };


        public static List<ProjectConfigItem> Create(bool devEnvironment, ScriptFilesStateType scriptFilesStateType)
        {
            List<ProjectConfigItem> projectConfigs = new List<ProjectConfigItem>();

            foreach (DBConnectionInfo dbConnectionInfo in _dbConnectionInfos)
            {
                ProjectConfigItem projectConfig = CreateBaseProjectConfig();

                projectConfig.DevEnvironment = devEnvironment;

                projectConfig.DBType = dbConnectionInfo.DBType;
                projectConfig.Server = dbConnectionInfo.Server;
                projectConfig.Username = dbConnectionInfo.Username;
                projectConfig.Password = dbConnectionInfo.Password;
                projectConfig.DBName = dbConnectionInfo.DBName;

                SetScriptsFolderPath(ref projectConfig, scriptFilesStateType);

                projectConfigs.Add(projectConfig);
            }

            return projectConfigs;
        }






        private static ProjectConfigItem CreateBaseProjectConfig()
        {
            return new ProjectConfigItem()
            {
                Id = IntegrationTestsSetting.TestProjectId,
                //DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal),
                //DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_Normal),
                DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeployArtifact_FolderPath),
                BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder),
            };
        }

        private static ProjectConfigItem SetScriptsFolderPath(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
        {
            switch (projectConfig.DBType)
            {
                case "SqlServer":

                    SetScriptsFolderPathForSqlServer(ref projectConfig, scriptFilesStateType);
                    break;

                default:
                    throw new Exception($"Invalid DBType: '{projectConfig.DBType}'");
            }

            return projectConfig;
        }

        private static void SetScriptsFolderPathForSqlServer(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
        {
            if (projectConfig.DevEnvironment)
            {
                switch (scriptFilesStateType)
                {
                    case ScriptFilesStateType.ValidScripts:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.MissingFile:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_MissingFile).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ScriptError:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_ScriptError).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ChangedHistoryFiles_Incremental:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ChangedHistoryFiles_Repeatable:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable).Replace("[DBType]", projectConfig.DBType);
                        break;

                    default:
                        throw new Exception($"Invalid ScriptFilesStateType: '{scriptFilesStateType}'");
                }
            }
            else
            {
                switch (scriptFilesStateType)
                {
                    case ScriptFilesStateType.ValidScripts:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_Normal).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.MissingFile:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_MissingFileh).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ScriptError:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_ScriptError).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ChangedHistoryFiles_Incremental:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ChangedHistoryFiles_Repeatable:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.WithDevDummyDataFiles:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_WithDevDummyDataFiles).Replace("[DBType]", projectConfig.DBType);
                        break;

                    default:
                        throw new Exception($"Invalid ScriptFilesStateType: '{scriptFilesStateType}'");
                }
            }
        }
    }
}
