using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableScriptFileType : ScriptFileTypeBase
    {
        private RuntimeScriptFileFactoryBase _runtimeScriptFileFactory;
        public override RuntimeScriptFileFactoryBase RuntimeScriptFileFactory
        {
            get
            {
                if (_runtimeScriptFileFactory == null)
                {
                    _runtimeScriptFileFactory = new RepeatableRuntimeScriptFileFactory();
                }

                return _runtimeScriptFileFactory;
            }
        }

        public override string FileTypeCode => "Repeatable";

        public override string Prefix => "rptScript";

        public override string FilenamePattern => Prefix + "_[ScriptName].sql";

        public override string RegexFilenamePattern => "^" + Prefix + "_" + "[a-zA-Z_0-9]{1,}.sql$";

    }
}
