using AutoVersionsDB.Core.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFilesComparer : RepeatableScriptFilesComparer
    {
        public DevDummyDataScriptFilesComparer(FileSystemScriptFiles fileSystemScriptFiles,
                                                DBExecutedFiles dbExecutedFiles)
            : base(fileSystemScriptFiles, dbExecutedFiles)
        {

        }


    }
}
