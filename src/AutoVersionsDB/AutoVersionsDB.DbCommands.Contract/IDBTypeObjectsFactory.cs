
namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBTypeObjectsFactory
    {
        DBType DBType { get; }

        IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo);
        IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo);

        IDBScriptsProvider CreateDBScriptsProvider();
    }
}
