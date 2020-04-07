using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Repeatable
{
    public class RepeatableScriptFileProperties : ScriptFilePropertiesBase
    {
        public override string SortKey => ScriptName;

        public RepeatableScriptFileProperties(string scriptName)
            :base(scriptName)
        {

        }

    }
}
