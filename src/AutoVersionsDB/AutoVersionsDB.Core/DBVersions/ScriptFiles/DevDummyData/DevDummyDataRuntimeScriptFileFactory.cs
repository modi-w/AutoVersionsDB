//namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
//{
//    public class DevDummyDataRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<DevDummyDataRuntimeScriptFile>
//    {

//        public override bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, DevDummyDataRuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile)
//        {
//            int nextOrderNum = 1;

//            if (prevRuntimeScriptFile != null)
//            {
//                nextOrderNum = prevRuntimeScriptFile.OrderNum + 1;
//            }

//            newRuntimeScriptFile = DevDummyDataRuntimeScriptFile.CreateByScriptName(folderPath, scriptName, nextOrderNum);

//            return newRuntimeScriptFile.IsValidFileName;
//        }

//        public override RuntimeScriptFile CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
//        {
//            return new DevDummyDataRuntimeScriptFile(folderPath, fileFullPath);
//        }
//    }
//}
