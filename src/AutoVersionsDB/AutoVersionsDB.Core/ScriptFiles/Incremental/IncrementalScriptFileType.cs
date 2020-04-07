using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalScriptFileType : ScriptFileTypeBase
    {
        public override string FileTypeCode => "Incremental";

        public const string C_ScriptFile_DatePattern = "yyyy-MM-dd";

        public override string Prefix => "incScript";

        public override string FilenamePattern => Prefix + "_" + "[yyyy]-[MM]-[dd].[num]_[ScriptName].sql";

        //http://regexstorm.net/tester
        public override string RegexFilenamePattern => "^" + Prefix + "_" + "20[0-9]{2}-[0-1]{1}[0-9]{1}-[0-3]{1}[0-9]{1}.[0-9]{3}_[a-zA-Z_0-9]{1,}.sql$";

    }

}

