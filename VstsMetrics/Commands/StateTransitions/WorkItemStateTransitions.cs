using System.Collections.Generic;
using VstsMetrics.Commands.CycleTime;

namespace VstsMetrics.Commands.StateTransitions
{
    public class WorkItemStateTransitions
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<StateTransition> Transitions { get; private set; }
        public WorkItemStateTransitions(int? id, string title, IEnumerable<StateTransition> transitions)
        {
            if (id != null)
                Id = id.Value;
            Title = title;
            Transitions = transitions;
        }

        public IEnumerable<WorkItemStateTransition> Flatten()
        {
            foreach (var t in Transitions)
            {
                yield return new WorkItemStateTransition(Id, Title, 
                    t.From, t.To, t.Date);
            }            
        }
    }
}