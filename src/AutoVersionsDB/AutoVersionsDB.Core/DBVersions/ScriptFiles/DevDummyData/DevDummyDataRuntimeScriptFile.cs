using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataRuntimeScriptFile : RepeatableRuntimeScriptFile
    {
        public override ScriptFileTypeBase ScriptFileType => ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
        protected DevDummyDataRuntimeScriptFile() { }

        public static new DevDummyDataRuntimeScriptFile CreateByScriptName(string folderPath, string scriptName, int orderNum)
        {
            return new DevDummyDataRuntimeScriptFile()
            {
                FolderPath = folderPath,
                ScriptName = scriptName,
                OrderNum = orderNum,
            };
        }

        public DevDummyDataRuntimeScriptFile(string folderPath, string fileFullPath)
          : base(folderPath, fileFullPath)
        {
        }



    }
}
