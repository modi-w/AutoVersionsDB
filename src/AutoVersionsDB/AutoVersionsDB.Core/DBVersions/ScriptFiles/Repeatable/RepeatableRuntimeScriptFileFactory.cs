namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableRuntimeScriptFileFactory : RuntimeScriptFileFactoryBase<RepeatableRuntimeScriptFile>
    {

        public override bool TryParseNextRuntimeScriptFileInstance(string folderPath, string scriptName, RepeatableRuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFileBase newRuntimeScriptFile)
        {
            int nextOrderNum = 1;

            if (prevRuntimeScriptFile != null)
            {
                nextOrderNum = prevRuntimeScriptFile.OrderNum + 1;
            }

            newRuntimeScriptFile = RepeatableRuntimeScriptFile.CreateByScriptName(folderPath, scriptName, nextOrderNum);

            return newRuntimeScriptFile.IsValidFileName;
        }

        public override RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string folderPath, string fileFullPath)
        {
            return new RepeatableRuntimeScriptFile(folderPath, fileFullPath);
        }

    }
}
