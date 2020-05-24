namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class ScriptFileTypeBase
    {
        public static ScriptFileTypeBase Create<TScriptFileType>()
                where TScriptFileType : ScriptFileTypeBase, new()
        {
            return new TScriptFileType();
        }



        public abstract string FileTypeCode { get; }

        public abstract string FilenamePattern { get; }

        public abstract string RegexFilenamePattern { get; }

        public abstract string Prefix { get; }
        public string RelativeFolderName => FileTypeCode;




    }
}
