//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class NotificationEngineContext
//    {
//        public EngineSettings EngineSettings { get; }
//        public ExecutionParams ExecutionParams { get; }


//        public NotificationEngineContext(EngineSettings engineSettings,
//                                        ExecutionParams executionParams)
//        {
//            EngineSettings = engineSettings;
//            ExecutionParams = executionParams;
//        }

//    }


//    public class NotificationEngineContext<TEngineSettings, TExecutionParams> : NotificationEngineContext
//        where TEngineSettings : EngineSettings
//        where TExecutionParams : ExecutionParams
//    {

//        public NotificationEngineContext(TEngineSettings engineSettings,
//                                        TExecutionParams executionParams)
//            : base(engineSettings, executionParams)
//        { 
//        }

//    }

//}
