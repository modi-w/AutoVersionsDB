using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class ScriptFilePropertiesBase
    {
        public abstract string SortKey { get; }

        public string ScriptName { get; private set; }

        public ScriptFilePropertiesBase(string scriptName)
        {
            ScriptName = scriptName;
        }
    }
}
