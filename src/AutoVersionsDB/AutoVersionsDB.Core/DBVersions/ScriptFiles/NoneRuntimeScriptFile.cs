using System;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class NoneRuntimeScriptFile : RuntimeScriptFileBase
    {
        public const string TargetNoneScriptFileName = "#None";


        public override string SortKey => throw new NotImplementedException();


        public override string Filename => TargetNoneScriptFileName;

        public override ScriptFileTypeBase ScriptFileType => throw new NotImplementedException();

        public override string FolderPath { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public NoneRuntimeScriptFile()
        {
        }


    }
}
