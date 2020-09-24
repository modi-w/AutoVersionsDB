namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class ScriptFileTypeBase
    {
        public static TScriptFileType Create<TScriptFileType>()
                where TScriptFileType : ScriptFileTypeBase, new()
        {
            return new TScriptFileType();
        }

        public abstract RuntimeScriptFileFactoryBase RuntimeScriptFileFactory { get; }



        public abstract string FileTypeCode { get; }

        public abstract string FilenamePattern { get; }

        public abstract string RegexFilenamePattern { get; }

        public abstract string Prefix { get; }
        public string RelativeFolderName => FileTypeCode;



    }
}
