namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemCycleTimeSummary
    {


        public WorkItemCycleTimeSummary(string v, double average, double moe)
        {
            CycleTime = v;
            AverageInHours = average;
            MarginOfError = moe;
        }

        public string CycleTime { get; private set; }
        public double AverageInHours { get; private set; }
        public double MarginOfError { get; private set; }
    }
}