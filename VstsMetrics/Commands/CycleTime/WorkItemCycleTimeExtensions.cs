using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace VstsMetrics.Commands.CycleTime
{
    public static class WorkItemCycleTimeExtensions
    {
        public static IEnumerable<WorkItemCycleTimeSummary> Summarise(this IEnumerable<WorkItemCycleTime> cycleTimes)
        {
            var elapsedAverage = cycleTimes.Average(ct => ct.ElapsedCycleTimeInHours);
            var workingAverage = cycleTimes.Average(ct => ct.ApproximateWorkingCycleTimeInHours);

            var elapsedMoe = cycleTimes.MarginOfError(ct => ct.ElapsedCycleTimeInHours);
            var workingMoe = cycleTimes.MarginOfError(ct => ct.ApproximateWorkingCycleTimeInHours);

            return new[] {
                new WorkItemCycleTimeSummary("Elapsed", elapsedAverage, elapsedMoe),
                new WorkItemCycleTimeSummary("Approximate Working Time", workingAverage, workingMoe)
            };
        }

        private static double MarginOfError(this IEnumerable<WorkItemCycleTime> cycleTimes, Func<WorkItemCycleTime, double> getter)
        {
            // http://www.dummies.com/education/math/statistics/how-to-calculate-the-margin-of-error-for-a-sample-mean/
            return cycleTimes.Select(getter).PopulationStandardDeviation()
                   / Math.Sqrt(cycleTimes.Count())
                   * 1.96;
        }
    }
}