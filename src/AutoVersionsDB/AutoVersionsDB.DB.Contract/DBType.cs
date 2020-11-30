namespace AutoVersionsDB.DB.Contract
{
    public class DBType
    {
        public string Code { get; }
        public string Name { get; }

        public DBType(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
