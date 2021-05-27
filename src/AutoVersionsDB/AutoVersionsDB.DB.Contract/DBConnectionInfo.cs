namespace AutoVersionsDB.DB.Contract
{
    public class DBConnectionInfo
    {
        public string DBType { get; }
        public string Server { get; }
        public string DBName { get; }
        public string Username { get; }
        public string Password { get; }

        public int Timeout { get; }


        public bool HasValues => !string.IsNullOrWhiteSpace(DBType)
                            && !string.IsNullOrWhiteSpace(Server)
                            && !string.IsNullOrWhiteSpace(DBName);

        public DBConnectionInfo(string dbType,
                                string serverInstance,
                                string dataBaseName,
                                string username,
                                string password,
                                int timeout)
        {
            DBType = dbType;
            Server = serverInstance;
            DBName = dataBaseName;
            Username = username;
            Password = password;
            Timeout = timeout;
        }


        public override string ToString()
        {
            return ToString(DBName);
        }

        public string ToString(string databaseName)
        {
            return $"DBType: '{DBType}', ServerInstance: '{Server}', DataBaseName: '{databaseName}', Username: '{Username}', Password: '{Password}'";
        }

    }
}
