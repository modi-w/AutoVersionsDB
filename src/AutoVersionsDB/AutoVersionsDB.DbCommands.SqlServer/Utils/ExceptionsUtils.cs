using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.DbCommands.SqlServer.Utils
{
    public static class ExceptionsUtils
    {
        public static T ThrowIfNull<T>(this T argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            return argument;
        }
    }
}
