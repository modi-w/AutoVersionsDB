using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public interface INotificationEngine
    {
        string EngineTypeName { get; }

        List<NotificationableActionStepBase> ProcessSteps { get; }
        NotificationableActionStepBase RollbackStep { get; }
        NotificationExecutersFactoryManager NotificationExecutersFactoryManager { get; }

        void Run(ExecutionParams executionParams);
    }

    public interface IFluentNotificationEngine : INotificationEngine
    {
        void SetEngineTypeName(string engineTypeName);
        void AppendProcessStep(NotificationableActionStepBase processSteps);
        void SetRollbackStep(NotificationableActionStepBase rollbackStep);
    }

    public class NotificationEngine<TProcessState> : INotificationEngine
        where TProcessState : ProcessStateBase, new()
    {
        public string EngineTypeName { get; protected set; }

        public List<NotificationableActionStepBase> ProcessSteps { get; protected set; }
        public NotificationableActionStepBase RollbackStep { get; protected set; }
        public NotificationExecutersFactoryManager NotificationExecutersFactoryManager { get; protected set; }


        public NotificationEngine(string engineTypeName,
                                    List<NotificationableActionStepBase> processSteps,
                                    NotificationableActionStepBase rollbackStep,
                                    NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            EngineTypeName = engineTypeName;
            ProcessSteps = processSteps;
            RollbackStep = rollbackStep;

            NotificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }



        public void Run(ExecutionParams executionParams)
        {
            int totalNumOfSteps = ProcessSteps.Count;

            ProcessStateBase processState = new TProcessState();
            processState.ExecutionParams = executionParams;

            processState.StartProcessDateTime = DateTime.Now;


            using (NotificationWrapperExecuter rootNotificationWrapperExecuter = NotificationExecutersFactoryManager.Reset(totalNumOfSteps))
            {
                if (!NotificationExecutersFactoryManager.HasError)
                {
                    foreach (NotificationableActionStepBase processStep in ProcessSteps)
                    {
                        rootNotificationWrapperExecuter.ExecuteStep(processStep, "", processState, null);

                        if (NotificationExecutersFactoryManager.HasError)
                        {
                            rollbackProcess(rootNotificationWrapperExecuter, processState, processStep.StepName);
                            break;
                        }
                    }
                }

            }

            if (!processState.EndProcessDateTime.HasValue)
            {
                processState.EndProcessDateTime = DateTime.Now;
            }
        }



        private void rollbackProcess(NotificationWrapperExecuter currentNotificationWrapperExecuter, ProcessStateBase processState, string stepName)
        {
            if (RollbackStep != null)
            {
                if (processState.CanRollback)
                {
                    NotificationExecutersFactoryManager.ClearAllInternalProcessState();
                    NotificationExecutersFactoryManager.RootNotificationStateItem.NumOfSteps++;
                    currentNotificationWrapperExecuter.ExecuteStep(RollbackStep, $"because error on {stepName}", processState, null);
                }
            }
        }


    }


    public class NotificationEngine<TProcessState, TExecutionParams> : NotificationEngine<TProcessState>
        where TProcessState : ProcessStateBase, new()
        where TExecutionParams : ExecutionParams
    {

        public NotificationEngine(string engineTypeName,
                                    List<NotificationableActionStepBase> processSteps,
                                    NotificationableActionStepBase rollbackStep,
                                    NotificationExecutersFactoryManager notificationExecutersFactoryManager)
            : base(engineTypeName, processSteps, rollbackStep, notificationExecutersFactoryManager)
        {
        }

        public void Run(TExecutionParams executionParams)
        {
            base.Run(executionParams);
        }
    }

    public class FluentNotificationEngineBase<TProcessState, TExecutionParams> : NotificationEngine<TProcessState, TExecutionParams>, IFluentNotificationEngine
        where TProcessState : ProcessStateBase, new()
        where TExecutionParams : ExecutionParams
    {

        public FluentNotificationEngineBase(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
            : base("", new List<NotificationableActionStepBase>(), null, notificationExecutersFactoryManager)
        {

        }


        public void SetEngineTypeName(string engineTypeName)
        {
            EngineTypeName = engineTypeName;
        }


        public void AppendProcessStep(NotificationableActionStepBase processStep)
        {
            ProcessSteps.Add(processStep);
        }

        public void SetRollbackStep(NotificationableActionStepBase rollbackStep)
        {
            RollbackStep = rollbackStep;
        }

    }


    public static class FluentNotificationEngineMethods
    {
        public static TFluentNotificationEngine EngineTypeName<TFluentNotificationEngine>(this TFluentNotificationEngine notificationEngine, string engineTypeName)
                 where TFluentNotificationEngine : IFluentNotificationEngine
        {
            notificationEngine.SetEngineTypeName(engineTypeName);

            return notificationEngine;
        }

        public static TFluentNotificationEngine ProcessStep<TFluentNotificationEngine>(this TFluentNotificationEngine notificationEngine, NotificationableActionStepBase processStep)
                where TFluentNotificationEngine : IFluentNotificationEngine
        {
            notificationEngine.AppendProcessStep(processStep);

            return notificationEngine;
        }

        public static TFluentNotificationEngine RollbackStep<TFluentNotificationEngine>(this TFluentNotificationEngine notificationEngine, NotificationableActionStepBase rollbackStep)
            where TFluentNotificationEngine : IFluentNotificationEngine
        {
            notificationEngine.SetRollbackStep(rollbackStep);

            return notificationEngine;
        }

    }
}


