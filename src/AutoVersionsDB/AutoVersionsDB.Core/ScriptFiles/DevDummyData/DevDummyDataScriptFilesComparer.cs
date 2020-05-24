using AutoVersionsDB.Core.ScriptFiles.Repeatable;


namespace AutoVersionsDB.Core.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFilesComparer : RepeatableScriptFilesComparer
    {
        public DevDummyDataScriptFilesComparer(ScriptFilesManager scriptFilesManager,
                                                DBExecutedFilesManager dbExecutedFilesManager)
            : base(scriptFilesManager, dbExecutedFilesManager)
        {

        }


    }
}
