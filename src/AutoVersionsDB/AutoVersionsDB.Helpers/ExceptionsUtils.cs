using System;

namespace AutoVersionsDB.Helpers
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
