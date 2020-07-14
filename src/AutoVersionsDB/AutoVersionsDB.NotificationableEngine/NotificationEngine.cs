using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public interface INotificationEngine : IDisposable
    {
        string EngineTypeName { get; }

        Dictionary<string, string> EngineMetaData { get; }

        List<NotificationableActionStepBase> ProcessSteps { get; }
        NotificationableActionStepBase RollbackStep { get; }
        NotificationExecutersFactoryManager NotificationExecutersFactoryManager { get; }

        void Prepare(NotificationableEngineConfig notificationableEngineConfig);

        void Run(ExecutionParams executionParams);
    }


    public abstract class NotificationEngine<TProcessState> : INotificationEngine
        where TProcessState : ProcessStateBase, new()
    {
        public abstract string EngineTypeName { get; }
        public Dictionary<string, string> EngineMetaData { get; }

        public List<NotificationableActionStepBase> ProcessSteps { get; }
        public NotificationableActionStepBase RollbackStep { get; }
        public NotificationExecutersFactoryManager NotificationExecutersFactoryManager { get; }

        public event EventHandler<PrepareEngineEventArgs> Preparing;
        public event EventHandler<PrepareEngineEventArgs> Prepared;

        public NotificationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    NotificationableActionStepBase rollbackStep)
        {
            RollbackStep = rollbackStep;

            ProcessSteps = new List<NotificationableActionStepBase>();

            NotificationExecutersFactoryManager = notificationExecutersFactoryManager;

            EngineMetaData = new Dictionary<string, string>
            {
                ["EngineTypeName"] = EngineTypeName
            };
        }

        public void Prepare(NotificationableEngineConfig notificationableEngineConfig)
        {
            RaisePreparing(notificationableEngineConfig);

            if (RollbackStep != null)
            {
                RollbackStep.Prepare(notificationableEngineConfig);
            }

            foreach (NotificationableActionStepBase processStep in ProcessSteps)
            {
                processStep.Prepare(notificationableEngineConfig);
            }

            RaisePrepared(notificationableEngineConfig);
        }

        private void RaisePreparing(NotificationableEngineConfig NotificationableEngineConfig)
        {
            OnPreparing(new PrepareEngineEventArgs(NotificationableEngineConfig));
        }
        protected virtual void OnPreparing(PrepareEngineEventArgs e)
        {
            this.Preparing?.Invoke(this, e);
        }

        private void RaisePrepared(NotificationableEngineConfig NotificationableEngineConfig)
        {
            OnPrepared(new PrepareEngineEventArgs(NotificationableEngineConfig));
        }
        protected virtual void OnPrepared(PrepareEngineEventArgs e)
        {
            this.Prepared?.Invoke(this, e);
        }


        public void Run(ExecutionParams executionParams)
        {
            int totalNumOfSteps = ProcessSteps.Count;

            ProcessStateBase processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            processState.SetEngineMetaData(this.EngineMetaData);

            using (NotificationWrapperExecuter rootNotificationWrapperExecuter = NotificationExecutersFactoryManager.Reset(totalNumOfSteps))
            {
                if (!NotificationExecutersFactoryManager.HasError)
                {
                    foreach (NotificationableActionStepBase processStep in ProcessSteps)
                    {
                        rootNotificationWrapperExecuter.ExecuteStep(processStep, "", processState, null);

                        if (NotificationExecutersFactoryManager.HasError)
                        {
                            RollbackProcess(rootNotificationWrapperExecuter, processState, processStep.StepName);
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



        private void RollbackProcess(NotificationWrapperExecuter currentNotificationWrapperExecuter, ProcessStateBase processState, string stepName)
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


        #region IDisposable

        private bool _disposed = false;

        ~NotificationEngine() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                if (RollbackStep != null)
                {
                    if (RollbackStep is IDisposable)
                    {
                        (RollbackStep as IDisposable).Dispose();
                    }
                }

                foreach (IDisposable processStep in ProcessSteps.Where(e=> e is IDisposable))
                {
                    processStep.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion


    }


    public abstract class NotificationEngine<TProcessState, TExecutionParams, TNotificationableEngineConfig> : NotificationEngine<TProcessState>
        where TProcessState : ProcessStateBase, new()
        where TExecutionParams : ExecutionParams
        where TNotificationableEngineConfig : NotificationableEngineConfig
    {

        public NotificationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    NotificationableActionStepBase rollbackStep)
            : base(notificationExecutersFactoryManager, rollbackStep)
        {
        }

        public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
        {
            base.Prepare(notificationableEngineConfig);
        }



        public void Run(TExecutionParams executionParams)
        {
            base.Run(executionParams);
        }
    }

}


