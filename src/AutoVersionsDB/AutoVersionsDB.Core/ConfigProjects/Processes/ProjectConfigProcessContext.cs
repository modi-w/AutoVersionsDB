using AutoVersionsDB.Core.Common;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{
    public class ProjectConfigProcessContext : CommonProcessContext
    {
        public override ProjectConfigItem ProjectConfig => (ProcessParams as ProjectConfigProcessParams).ProjectConfig;


        public override bool CanRollback => false;



        public ProjectConfigProcessContext()
        {
        }

    }
}
