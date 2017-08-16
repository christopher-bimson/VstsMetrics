using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Abstractions;
using VstsMetrics.Extensions;

namespace VstsMetrics.Commands.StateTransitions
{
    public class WorkItemStateTransitionAggregator : WorkItemAggregator<WorkItemStateTransitions>
    {
        public WorkItemStateTransitionAggregator(IWorkItemClient workItemClient, int batchSize = 50) : base(workItemClient, batchSize)
        {
        }

        protected override async Task<WorkItemStateTransitions> AggregatorAsync(WorkItem workItem, DateTime? sinceDate)
        {
            if (workItem?.Id == null)
                return null;

            var revisions = await WorkItemClient.GetWorkItemRevisionsAsync(workItem);
            return new WorkItemStateTransitions(workItem.Id, workItem.Title(), revisions.StateTransitions());
        }
    }
}