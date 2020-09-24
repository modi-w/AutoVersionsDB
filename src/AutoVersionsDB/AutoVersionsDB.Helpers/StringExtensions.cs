﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.Helpers
{
    public static class StringExtensions
    {
        public static string ToTrimedInvariant(this string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                return str.Trim().ToUpperInvariant();
            }
        }

        public static string Ellipsis(this string str, int maxLength)
        {
            string resultsStr = str;

            if (str.Length > maxLength)
            {
                resultsStr = $"{str.Substring(0, maxLength - 3)}...";
            }

            return resultsStr;
        }
    }
}
