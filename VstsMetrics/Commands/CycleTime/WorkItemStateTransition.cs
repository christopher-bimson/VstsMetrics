using System;

namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemStateTransition
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime Date { get; private set; }

        public WorkItemStateTransition(string from, string to, DateTime date)
        {
            From = @from;
            To = to;
            Date = date;
        }
    }
}