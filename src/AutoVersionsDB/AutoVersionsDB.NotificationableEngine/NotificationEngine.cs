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

        //void Prepare(NotificationableEngineConfig notificationableEngineConfig);

        ProcessTrace Run(NotificationableEngineConfig notificationableEngineConfig, ExecutionParams executionParams, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged);
    }


    public abstract class NotificationEngine<TProcessState> : INotificationEngine
        where TProcessState : ProcessStateBase, new()
    {
        private readonly NotificationExecutersProviderFactory _notificationExecutersProviderFactory;

        public abstract string EngineTypeName { get; }
        public Dictionary<string, string> EngineMetaData { get; }

        public List<NotificationableActionStepBase> ProcessSteps { get; }
        public NotificationableActionStepBase RollbackStep { get; }

        public event EventHandler<InitiateEngineEventArgs> Initiated;

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


        private void RaiseInitiated(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processStateBase)
        {
            OnInitiated(new InitiateEngineEventArgs(notificationableEngineConfig, processStateBase));
        }
        protected virtual void OnInitiated(InitiateEngineEventArgs e)
        {
            this.Initiated?.Invoke(this, e);
        }


        public ProcessTrace Run(NotificationableEngineConfig notificationableEngineConfig, ExecutionParams executionParams, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateBase processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            processState.SetEngineMetaData(this.EngineMetaData);

            RaiseInitiated(notificationableEngineConfig, processState);


            NotificationExecutersProvider notificationExecutersProvider = _notificationExecutersProviderFactory.Create(onNotificationStateChanged);

            using (NotificationWrapperExecuter rootNotificationWrapperExecuter = notificationExecutersProvider.Reset(ProcessSteps, false))
            {
                rootNotificationWrapperExecuter.Execute(notificationableEngineConfig, notificationExecutersProvider, processState);

                if (notificationExecutersProvider.ProcessTrace.HasError)
                {
                    RollbackProcess(notificationExecutersProvider, notificationableEngineConfig, processState);
                }
            }

            if (!processState.EndProcessDateTime.HasValue)
            {
                processState.EndProcessDateTime = DateTime.Now;
            }

            return notificationExecutersProvider.ProcessTrace;
        }



        private void RollbackProcess(NotificationExecutersProvider notificationExecutersProvider, NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        {
            if (RollbackStep != null)
            {
                if (processState.CanRollback)
                {
                    notificationExecutersProvider.ClearAllInternalProcessState();
                    notificationExecutersProvider.NotifictionStateChangeHandler.RootNotificationStateItem.NumOfSteps++;

                    //כרגע הבעיה היא שה- restore רץ כאילו הוא sub step של ה- step האחרון שרץ

                    using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(RollbackStep.StepName, new List<NotificationableActionStepBase>() { RollbackStep }, true))
                    {
                        notificationWrapperExecuter.Execute(notificationableEngineConfig, notificationExecutersProvider, processState);
                    }
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

        //public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
        //{
        //    base.Prepare(notificationableEngineConfig);
        //}



        public ProcessTrace Run(TNotificationableEngineConfig notificationableEngineConfig, TExecutionParams executionParams, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            return base.Run(notificationableEngineConfig as NotificationableEngineConfig, executionParams, onNotificationStateChanged);
        }
    }

}


