using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalScriptFileProperties : ScriptFilePropertiesBase
    {
        public const string C_ScriptFile_DatePattern = "yyyy-MM-dd";

        public override string SortKey => $"{Date.ToString(C_ScriptFile_DatePattern,CultureInfo.InvariantCulture)}{Version:000}{ScriptName}";

        public DateTime Date { get; private set; }
        public int Version { get; private set; }


        public IncrementalScriptFileProperties(string scriptName, DateTime date, int version)
            : base(scriptName)
        {
            Date = date;
            Version = version;
        }

    }
}
