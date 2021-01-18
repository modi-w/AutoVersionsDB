using System;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class LastRuntimeScriptFile : RuntimeScriptFileBase
    {
        public const string TargetLastScriptFileName= "#Last";


        public override string SortKey => throw new NotImplementedException();


        public override string Filename => TargetLastScriptFileName;

        public override ScriptFileTypeBase ScriptFileType => throw new NotImplementedException();

        public override string FolderPath { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public LastRuntimeScriptFile()
        {
        }


    }
}
