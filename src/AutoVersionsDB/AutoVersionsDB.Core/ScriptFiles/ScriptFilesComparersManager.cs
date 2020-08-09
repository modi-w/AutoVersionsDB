//using AutoVersionsDB.Core.ConfigProjects;
//using AutoVersionsDB.Core.Utils;
//using AutoVersionsDB.DbCommands.Integration;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AutoVersionsDB.Core.ScriptFiles
//{
//    public class ScriptFilesComparersManager : IDisposable
//    {
//        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
//        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;

//        public Dictionary<string, ScriptFilesState> ScriptFilesComparersProviders { get; }


//        public ScriptFilesComparersManager(DBCommandsFactoryProvider dbCommandsFactoryProvider,
//                                            ScriptFilesComparerFactory scriptFilesComparerFactory)
//        {
//            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
//            scriptFilesComparerFactory.ThrowIfNull(nameof(scriptFilesComparerFactory));

//            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
//            _scriptFilesComparerFactory = scriptFilesComparerFactory;

//            ScriptFilesComparersProviders = new Dictionary<string, ScriptFilesState>();
//        }

//        public void Load(ProjectConfigItem projectConfig)
//        {
//            projectConfig.ThrowIfNull(nameof(projectConfig));

//            if (!ScriptFilesComparersProviders.TryGetValue(projectConfig.ProjectGuid, out ScriptFilesState processTrace))
//            {
//                processTrace = new ScriptFilesState(_dbCommandsFactoryProvider, _scriptFilesComparerFactory, projectConfig);
//                ScriptFilesComparersProviders.Add(projectConfig.ProjectGuid, processTrace);
//            }

//            processTrace.Reload();
//        }

//        public ScriptFilesState GetScriptFilesState(string projectGuid)
//        {
//            return ScriptFilesComparersProviders[projectGuid];
//        }




//        #region IDisposable

//        private bool _disposed = false;

//        ~ScriptFilesComparersManager() => Dispose(false);

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (_disposed)
//            {
//                return;
//            }

//            if (disposing)
//            {
//                //foreach (var sriptFilesComparerProvider in ScriptFilesComparersProviders.Values)
//                //{
//                //    sriptFilesComparerProvider.Dispose();
//                //}
//            }

//            _disposed = true;
//        }

//        #endregion

//    }
//}
