namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemCycleTimeSummary
    {
        private double _elapsedAverage;
        private double _workingAverage;

        public WorkItemCycleTimeSummary(double elapsedAverage, double workingAverage)
        {
            this._elapsedAverage = elapsedAverage;
            this._workingAverage = workingAverage;
        }

        public string AverageElapsedCycleTimeInHours
        {
            get
            {
                return _elapsedAverage.ToString();
            }
        }

        public string AverageApproximateWorkingCycleTimeInHours
        {
            get
            {
                return _workingAverage.ToString();
            }
        }
    }
}