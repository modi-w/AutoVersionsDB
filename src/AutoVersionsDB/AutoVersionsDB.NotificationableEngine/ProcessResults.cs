namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessResults
    {
        public ProcessTrace Trace { get; }
        public object Results { get; }

        internal ProcessResults(ProcessTrace trace, object results)
        {
            Trace = trace;
            Results = results;
        }
    }
}
