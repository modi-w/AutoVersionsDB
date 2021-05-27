namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class ScriptFileTypeBase
    {
        public static TScriptFileType Create<TScriptFileType>()
                where TScriptFileType : ScriptFileTypeBase, new()
        {
            return new TScriptFileType();
        }



        public abstract string FileTypeCode { get; }

        public string FilenamePattern => Prefix + "_[OrderNum]_[ScriptName].sql";

        //http://regexstorm.net/tester
        public string RegexFilenamePattern => "^" + Prefix + "_" + "[0-9]{4}_[a-zA-Z_0-9]{1,}.sql$";

        public abstract string Prefix { get; }
        public string RelativeFolderName => FileTypeCode;



    }
}
