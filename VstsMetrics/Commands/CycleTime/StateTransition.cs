using System;

namespace VstsMetrics.Commands.CycleTime
{
    public class StateTransition
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime Date { get; private set; }

        public StateTransition(string from, string to, DateTime date)
        {
            From = @from;
            To = to;
            Date = date;
        }
    }
}