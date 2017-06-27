using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using WorkItemStateTransition = VstsMetrics.Commands.CycleTime.WorkItemStateTransition;

namespace VstsMetrics.Extensions
{
    public static class WorkItemExtensions
    {
        public static string State(this WorkItem workItem)
        {
            return workItem.Fields["System.State"].ToString();
        }

        public static DateTime ChangedDate(this WorkItem workItem)
        {
            return (DateTime)workItem.Fields["System.ChangedDate"];
        }

        public static string Title(this WorkItem workItem)
        {
            return workItem.Fields["System.Title"].ToString();
        }

        public static string Tags(this WorkItem workItem)
        {
            if (workItem.Fields.ContainsKey("System.Tags"))
                return workItem.Fields["System.Tags"].ToString();
            return String.Empty;
        }

        public static DateTime? FirstStateTransitionFrom(this IEnumerable<WorkItem> workItemRevisions, string state)
        {
            var stateTransitions = GetWorkItemStateTransitions(workItemRevisions);
            return
                stateTransitions
                    .FirstOrDefault(t => t.From.Equals(state, StringComparison.InvariantCultureIgnoreCase))?.Date;
        }

        public static DateTime? LastStateTransitionFrom(this IEnumerable<WorkItem> workItemRevisions, string state)
        {
            var stateTransitions = GetWorkItemStateTransitions(workItemRevisions);
            return
                stateTransitions
                    .LastOrDefault(t => t.From.Equals(state, StringComparison.InvariantCultureIgnoreCase))?.Date;
        }

        public static DateTime? LastStateTransitionTo(this IEnumerable<WorkItem> workItemRevisions, string state)
        {
            var stateTransitions = GetWorkItemStateTransitions(workItemRevisions);
            return
                stateTransitions
                    .LastOrDefault(t => t.To.Equals(state, StringComparison.InvariantCultureIgnoreCase))?.Date;
        }

        private static IEnumerable<WorkItemStateTransition> GetWorkItemStateTransitions(IEnumerable<WorkItem> workItemRevisions)
        {
            workItemRevisions = workItemRevisions.OrderBy(wi => wi.Rev);
            GuardAllRevisionsOfSameWorkItem(workItemRevisions);

            string lastState = null;
            var stateTransitions = new List<WorkItemStateTransition>();
            foreach (var revision in workItemRevisions)
            {
                if (lastState == null)
                {
                    lastState = revision.State();
                    continue;
                }

                if (!revision.State().Equals(lastState, StringComparison.InvariantCultureIgnoreCase))
                {
                    stateTransitions.Add(new WorkItemStateTransition(lastState, revision.State(), revision.ChangedDate()));
                    lastState = revision.State();
                }
            }
            return stateTransitions.OrderBy(t => t.Date);
        }

        private static void GuardAllRevisionsOfSameWorkItem(IEnumerable<WorkItem> workItemRevisions)
        {
            if (workItemRevisions.Any(wi => wi.Id != workItemRevisions.First().Id))
                throw new ArgumentException("These are not all revisions of the same work item.",
                    nameof(workItemRevisions));
        }

    }
}