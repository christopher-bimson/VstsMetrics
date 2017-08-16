using System;

namespace VstsMetrics.Commands.StateTransitions
{
    public class WorkItemStateTransition
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Date { get; private set; }

        public WorkItemStateTransition(int id, string title, string from, string to, DateTime transitionDate)
        {
            Id = id;
            Title = title;
            From = from;
            To = to;
            Date = transitionDate.ToString();
        }        
    }
}