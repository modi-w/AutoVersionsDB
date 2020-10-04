using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Process
{

    public static class ProjectConfigsFactory
    {
        private static List<DBConnectionInfo> _dbConnectionInfos = new List<DBConnectionInfo>()
        {
            new DBConnectionInfo("SqlServer",
                                SqlServerLocalDBConnection.ConnectionStringBuilder.DataSource,
                                "AutoVersionsDB.Tests",
                                SqlServerLocalDBConnection.ConnectionStringBuilder.UserID,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.Password)
        };


        /// <summary>
        /// Create an array of project config for each exist DBType.
        /// </summary>
        /// <param name="devEnvironment"></param>
        /// <param name="scriptFilesStateType"></param>
        /// <returns></returns>
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
                Id = IntegrationTestsConsts.TestProjectId,
                //DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal),
                //DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeliveryArtifactFolderPath_Normal),
                DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeployArtifact_FolderPath),
                BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackupBaseFolder),
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


            projectConfig.DeployArtifactFolderPath = projectConfig.DeployArtifactFolderPath.Replace("[DBType]", projectConfig.DBType);

            return projectConfig;
        }

        private static void SetScriptsFolderPathForSqlServer(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
        {
            if (projectConfig.DevEnvironment)
            {
                switch (scriptFilesStateType)
                {
                    case ScriptFilesStateType.ValidScripts:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.MissingFile:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_MissingFile).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ScriptError:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_ScriptError).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.IncrementalChanged:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.RepeatableChanged:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable).Replace("[DBType]", projectConfig.DBType);
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

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_Normal).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.MissingFile:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_MissingFileh).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.ScriptError:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ScriptError).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.IncrementalChanged:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.RepeatableChanged:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable).Replace("[DBType]", projectConfig.DBType);
                        break;

                    case ScriptFilesStateType.WithDevDummyDataFiles:

                        projectConfig.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_WithDevDummyDataFiles).Replace("[DBType]", projectConfig.DBType);
                        break;

                    default:
                        throw new Exception($"Invalid ScriptFilesStateType: '{scriptFilesStateType}'");
                }
            }
        }
    }
}
