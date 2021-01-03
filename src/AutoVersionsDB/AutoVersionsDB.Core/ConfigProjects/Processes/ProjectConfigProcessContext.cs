using AutoVersionsDB.Core.Common;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{
    public class ProjectConfigProcessContext : CommonProcessContext
    {
        public override ProjectConfigItem ProjectConfig => (ProcessArgs as ProjectConfigProcessArgs).ProjectConfig;


        public override bool CanRollback => false;



        public ProjectConfigProcessContext()
        {
        }

    }
}
