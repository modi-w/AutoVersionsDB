using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public static class ResetDBStepFluent
    {
        public static AutoVersionsDbEngine ResetDB(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                                    IDBCommands dbCommands, 
                                                                    bool isDevEnvironment)
        {
            ResetDBStep resetDBStep =
                new ResetDBStep(dbCommands,
                                            isDevEnvironment);


            autoVersionsDbEngine.AppendProcessStep(resetDBStep);

            return autoVersionsDbEngine;
        }
    }

    public class ResetDBStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Resolve Reset Database";

        private IDBCommands _dbCommands;

        private bool _isDevEnvironment;


        public ResetDBStep(IDBCommands dbCommands, bool isDevEnvironment)
        {
            _dbCommands = dbCommands;
            _isDevEnvironment = isDevEnvironment;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            //if (processState.IsResetDBFlag) 
            //{
                if (!_isDevEnvironment)
                {
                    throw new Exception("Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).");
                }

                _dbCommands.DropAllDB();

                //_dbCommands.RecreateDBVersionsTables();
            //}
        }



    }
}
