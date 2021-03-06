﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{

    public class ProjectConfigsFactory
    {
        private readonly List<DBConnectionInfo> _dbConnectionInfos = new List<DBConnectionInfo>()
        {
            new DBConnectionInfo(IntegrationTestsConsts.SqlServerDBType,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.DataSource,
                                IntegrationTestsConsts.TestDBName,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.UserID,
                                SqlServerLocalDBConnection.ConnectionStringBuilder.Password,
                                0)
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






        private static ProjectConfigItem CreateBaseProjectConfig()
        {
            return new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeployArtifact_FolderPath),
                BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackupBaseFolder),
            };
        }

        public virtual ProjectConfigItem SetFoldersPathByDBType(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
        {
            SetScriptsFoldersPath(ref projectConfig, scriptFilesStateType);

            projectConfig.DeployArtifactFolderPath = projectConfig.DeployArtifactFolderPath.Replace("[DBType]", projectConfig.DBType);

            return projectConfig;
        }



        private static void SetScriptsFoldersPath(ref ProjectConfigItem projectConfig, ScriptFilesStateType scriptFilesStateType)
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

                    case ScriptFilesStateType.NoScriptFiles:

                        projectConfig.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_NoScriptFiles).Replace("[DBType]", projectConfig.DBType);

                        if (!Directory.Exists(projectConfig.IncrementalScriptsFolderPath))
                        {
                            Directory.CreateDirectory(projectConfig.IncrementalScriptsFolderPath);
                        }
                        if (!Directory.Exists(projectConfig.RepeatableScriptsFolderPath))
                        {
                            Directory.CreateDirectory(projectConfig.RepeatableScriptsFolderPath);
                        }
                        if (!Directory.Exists(projectConfig.DevDummyDataScriptsFolderPath))
                        {
                            Directory.CreateDirectory(projectConfig.DevDummyDataScriptsFolderPath);
                        }

                        break;

                    default:
                        throw new Exception($"Invalid ScriptFilesStateType: '{scriptFilesStateType}'");
                }
            }
            else
            {
                projectConfig.DeliveryArtifactFolderPath = scriptFilesStateType switch
                {
                    ScriptFilesStateType.ValidScripts => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_Normal).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.MissingFile => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_MissingFileh).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.ScriptError => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ScriptError).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.IncrementalChanged => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.RepeatableChanged => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.WithDevDummyDataFiles => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_WithDevDummyDataFiles).Replace("[DBType]", projectConfig.DBType),
                    ScriptFilesStateType.NoScriptFiles => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_NoScriptFiles).Replace("[DBType]", projectConfig.DBType),
                    _ => throw new Exception($"Invalid ScriptFilesStateType: '{scriptFilesStateType}'"),
                };
            }
        }
    }
}
