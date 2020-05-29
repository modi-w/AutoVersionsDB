using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class RuntimeScriptFileBase
    {
        public abstract ScriptFilePropertiesBase ScriptFileProperties { get; }


        public abstract string SortKey { get; }

        public abstract string FileTypeCode { get; }

        public abstract string Filename { get; }
        public abstract string FileFullPath { get; }

        public string ComputedHash { get; set; }
        public DateTime ComputedHashDateTime { get; set; }
        public eHashDiffType HashDiffType { get; set; }

        public DateTime ExecutedDateTime { get; set; }


        protected abstract void parsePropertiesByFileFullPath(string fileFullPath);

        protected RuntimeScriptFileBase() { }
    }

    public abstract class RuntimeScriptFileBase<TScriptFileProperties> : RuntimeScriptFileBase
        where TScriptFileProperties : ScriptFilePropertiesBase
    {
        protected string _folderPath;

        public ScriptFileTypeBase ScriptFileType { get; private set; }

        public override ScriptFilePropertiesBase ScriptFileProperties => _scriptFileProperties;
        protected TScriptFileProperties _scriptFileProperties { get; set; }

        public override string SortKey => _scriptFileProperties.SortKey;

        public override string FileTypeCode => ScriptFileType.FileTypeCode;

        public override string FileFullPath => Path.Combine(_folderPath, Filename);


        private RuntimeScriptFileBase(ScriptFileTypeBase scriptFileType, string folderPath)
        {
            ScriptFileType = scriptFileType;
            _folderPath = folderPath;
        }

        public RuntimeScriptFileBase(ScriptFileTypeBase scriptFileType, string folderPath, TScriptFileProperties scriptFileProperties)
            : this(scriptFileType, folderPath)
        {
            _scriptFileProperties = scriptFileProperties;
        }

        public RuntimeScriptFileBase(ScriptFileTypeBase scriptFileType, string folderPath, string fileFullPath)
            : this(scriptFileType, folderPath)
        {
            parsePropertiesByFileFullPath(fileFullPath);
        }

    }
}
