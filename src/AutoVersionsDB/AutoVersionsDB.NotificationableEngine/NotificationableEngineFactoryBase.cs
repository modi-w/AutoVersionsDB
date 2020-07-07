//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public abstract class NotificationableEngineFactoryBase
//    {
//        public abstract string EngineTypeName { get; }

//        private Dictionary<INotificationEngine, List<IDisposable>> _disposablesDictionaryByStep;
//        private object _disposablesSync;


//        public NotificationableEngineFactoryBase()
//        {
//            _disposablesDictionaryByStep = new Dictionary<INotificationEngine, List<IDisposable>>();
//            _disposablesSync = new object();
//        }


//        public abstract INotificationEngine Create(NotificationableEngineConfig notificationableEngineConfig);


//        protected void AddDisposableReferenceForEngine(INotificationEngine engine, IDisposable disposable)
//        {
//            lock (_disposablesSync)
//            {
//                List<IDisposable> engineDisposables;
//                if (!_disposablesDictionaryByStep.TryGetValue(engine, out engineDisposables))
//                {
//                    engineDisposables = new List<IDisposable>();
//                    _disposablesDictionaryByStep.Add(engine, engineDisposables);
//                }

//                engineDisposables.Add(disposable);
//            }
//        }

//        public void ReleaseEngine(INotificationEngine engine)
//        {
//            lock (_disposablesSync)
//            {
//                List<IDisposable> engineDisposables;
//                if (_disposablesDictionaryByStep.TryGetValue(engine, out engineDisposables))
//                {
//                    foreach (IDisposable disposable in engineDisposables)
//                    {
//                        disposable.Dispose();
//                    }
//                }

//            }
//        }
//    }

//    public abstract class NotificationableEngineFactoryBase<TNotificationEngine, TNotificationableEngineConfig> : NotificationableEngineFactoryBase
//        where TNotificationEngine: INotificationEngine
//        where TNotificationableEngineConfig : NotificationableEngineConfig
//    {

//        public override INotificationEngine Create(NotificationableEngineConfig notificationableEngineConfig)
//        {
//            return this.Create(notificationableEngineConfig as TNotificationableEngineConfig);
//        }

//        public abstract TNotificationEngine Create(TNotificationableEngineConfig notificationableEngineConfig);
//    }


//}
