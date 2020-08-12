using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.DbCommands.Contract
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
