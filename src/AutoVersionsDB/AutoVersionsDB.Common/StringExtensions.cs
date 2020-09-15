using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.Common
{
    public static class StringExtensions
    {
        public static string ToTrimedInvariant(this string str)
        {
            if (str== null)
            {
                return "";
            }
            else
            {
                return str.Trim().ToUpperInvariant();
            }
        }
    }
}
