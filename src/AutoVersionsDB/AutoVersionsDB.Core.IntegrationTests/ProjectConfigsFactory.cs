using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{

    public class ProjectConfigsFactory
    {
        private List<DBConnectionInfo> _dbConnectionInfos = new List<DBConnectionInfo>()
        {
            new DBConnectionInfo(IntegrationTestsConsts.SqlServerDBType,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.DataSource,
                                IntegrationTestsConsts.TestDBName,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.UserID,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.Password)
        };


        /// <summary>
        /// Create an array of project config for each exist DBType.
        /// </summary>
        /// <param name="devEnvironment"></param>
        /// <param name="scriptFilesStateType"></param>
        /// <returns></returns>
        public List<ProjectConfigItem> CreateProjectConfigsByDBTyps()
        {
            List<ProjectConfigItem> projectConfigs = new List<ProjectConfigItem>();

            foreach (DBConnectionInfo dbConnectionInfo in _dbConnectionInfos)
            {
                ProjectConfigItem projectConfig = CreateBaseProjectConfig();


                projectConfig.DBType = dbConnectionInfo.DBType;
                projectConfig.Server = dbConnectionInfo.Server;
                projectConfig.Username = dbConnectionInfo.Username;
                projectConfig.Password = dbConnectionInfo.Password;
                projectConfig.DBName = dbConnectionInfo.DBName;

                //projectConfig.DevEnvironment = devEnvironment;
                //SetFoldersPatByDBType(ref projectConfig, scriptFilesStateType);

                projectConfigs.Add(projectConfig);
            }

            return projectConfigs;
        }






        private ProjectConfigItem CreateBaseProjectConfig()
        {
            return new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeployArtifact_FolderPath),
                BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackupBaseFolder),
            };
        }

        public ProjectConfigItem SetFoldersPathByDBType(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
        {
            SetScriptsFoldersPath(ref projectConfig, scriptFilesStateType);

            projectConfig.DeployArtifactFolderPath = projectConfig.DeployArtifactFolderPath.Replace("[DBType]", projectConfig.DBType);

            return projectConfig;
        }



        private void SetScriptsFoldersPath(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
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
