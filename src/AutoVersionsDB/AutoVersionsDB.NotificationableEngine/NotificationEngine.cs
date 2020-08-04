using AutoVersionsDB.NotificationableEngine.Utils;
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

        void Prepare(NotificationableEngineConfig notificationableEngineConfig);

        ProcessStateResults Run(ExecutionParams executionParams, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged);
    }


    public abstract class NotificationEngine<TProcessState> : INotificationEngine
        where TProcessState : ProcessStateBase, new()
    {
        private NotificationExecutersProviderFactory _notificationExecutersProviderFactory;

        public abstract string EngineTypeName { get; }
        public Dictionary<string, string> EngineMetaData { get; }

        public List<NotificationableActionStepBase> ProcessSteps { get; }
        public NotificationableActionStepBase RollbackStep { get; }

        public event EventHandler<PrepareEngineEventArgs> Preparing;
        public event EventHandler<PrepareEngineEventArgs> Prepared;

        public NotificationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                    NotificationableActionStepBase rollbackStep)
        {
            notificationExecutersProviderFactory.ThrowIfNull(nameof(notificationExecutersProviderFactory));

            RollbackStep = rollbackStep;

            ProcessSteps = new List<NotificationableActionStepBase>();

            _notificationExecutersProviderFactory = notificationExecutersProviderFactory;

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


        public ProcessStateResults Run(ExecutionParams executionParams, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            int totalNumOfSteps = ProcessSteps.Count;

            ProcessStateBase processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            processState.SetEngineMetaData(this.EngineMetaData);

            NotificationExecutersProvider notificationExecutersProvider = _notificationExecutersProviderFactory.Create(onNotificationStateChanged);

            using (NotificationWrapperExecuter rootNotificationWrapperExecuter = notificationExecutersProvider.Reset(totalNumOfSteps))
            {
                if (!notificationExecutersProvider.NotifictionStatesHistory.HasError)
                {
                    foreach (NotificationableActionStepBase processStep in ProcessSteps)
                    {
                        rootNotificationWrapperExecuter.ExecuteStep(processStep, "", processState, null);

                        if (notificationExecutersProvider.NotifictionStatesHistory.HasError)
                        {
                            RollbackProcess(notificationExecutersProvider, rootNotificationWrapperExecuter, processState, processStep.StepName);
                            break;
                        }
                    }
                }

            }

            if (!processState.EndProcessDateTime.HasValue)
            {
                processState.EndProcessDateTime = DateTime.Now;
            }

            return notificationExecutersProvider.NotifictionStatesHistory;
        }



        private void RollbackProcess(NotificationExecutersProvider notificationExecutersProvider, NotificationWrapperExecuter currentNotificationWrapperExecuter, ProcessStateBase processState, string stepName)
        {
            if (RollbackStep != null)
            {
                if (processState.CanRollback)
                {
                    notificationExecutersProvider.ClearAllInternalProcessState();
                    notificationExecutersProvider.RootNotificationStateItem.NumOfSteps++;
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

                foreach (IDisposable processStep in ProcessSteps.Where(e => e is IDisposable))
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

        public NotificationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                    NotificationableActionStepBase rollbackStep)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
        }

        public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
        {
            base.Prepare(notificationableEngineConfig);
        }



        public ProcessStateResults Run(TExecutionParams executionParams, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            return base.Run(executionParams, onNotificationStateChanged);
        }
    }

}


