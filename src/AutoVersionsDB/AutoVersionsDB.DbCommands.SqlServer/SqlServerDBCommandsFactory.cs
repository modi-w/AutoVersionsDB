using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBCommandsFactory : IDBCommandsFactory
    {
        private ConcurrentDictionary<object, List<IDisposable>> _internalDisposableServices;


        public DBType DBType
        {
            get
            {
                return new DBType("SqlServer", "Sql Server");
            }
        }


        public SqlServerDBCommandsFactory()
        {
            _internalDisposableServices = new ConcurrentDictionary<object, List<IDisposable>>();

        }


        public IDBConnection CreateDBConnection(string connectionString, int timeout)
        {
            return CreateDBConnection(connectionString, timeout, false);
        }
        private IDBConnection CreateDBConnection(string connectionString, int timeout, bool isInternalCall)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(connectionString, timeout);

            ResolveAddToDisposables(sqlServerConnection, sqlServerConnection, isInternalCall);

            return sqlServerConnection;
        }


        public IDBCommands CreateDBCommands(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = CreateDBConnection(connectionString, timeout, true) as SqlServerConnection;
            SqlServerDBCommands sqlServerDBCommands = new SqlServerDBCommands(sqlServerConnection);

            ResolveAddToDisposables(sqlServerDBCommands, sqlServerConnection, false);


            return sqlServerDBCommands;
        }

        public IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = CreateDBConnection(connectionString, timeout, true) as SqlServerConnection;
            SqlServerDBBackupRestoreCommands sqlServerDBBackupRestoreCommands = new SqlServerDBBackupRestoreCommands(sqlServerConnection);

            ResolveAddToDisposables(sqlServerDBBackupRestoreCommands, sqlServerConnection, false);


            return sqlServerDBBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = CreateDBConnection(connectionString, timeout, true) as SqlServerConnection;
            SqlServerDBQueryStatus sqlServerDBQueryStatus = new SqlServerDBQueryStatus(sqlServerConnection);

            ResolveAddToDisposables(sqlServerDBQueryStatus, sqlServerConnection, false);


            return sqlServerDBQueryStatus;
        }



        private void ResolveAddToDisposables(object retrunInstance, object internalService, bool isInternalCall)
        {
            if (!isInternalCall)
            {
                IDisposable disposableInternalService = internalService as IDisposable;

                if (disposableInternalService is IDisposable)
                {
                    List<IDisposable> internalDisposables;
                    if (!_internalDisposableServices.TryGetValue(retrunInstance, out internalDisposables))
                    {
                        internalDisposables = new List<IDisposable>();
                        _internalDisposableServices[retrunInstance] = internalDisposables;
                    }

                    internalDisposables.Add(disposableInternalService);
                }
            }

        }


        public void ReleaseService(object serviceToRelease)
        {
            List<IDisposable> internalDisposables;
            if (_internalDisposableServices.TryRemove(serviceToRelease, out internalDisposables))
            {
                foreach (IDisposable disposableInternalService in internalDisposables)
                {
                    disposableInternalService.Dispose();
                }

                internalDisposables.Clear();
            }
        }
    }
}
