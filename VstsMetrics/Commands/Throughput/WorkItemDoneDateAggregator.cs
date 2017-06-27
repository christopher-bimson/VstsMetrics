using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Abstractions;
using VstsMetrics.Extensions;

namespace VstsMetrics.Commands.Throughput
{
    public class WorkItemDoneDateAggregator : WorkItemAggregator<WorkItemDoneDate>
    {
        private readonly string _doneState;

        public WorkItemDoneDateAggregator(IWorkItemClient workItemClient, string doneState, 
            int batchSize = 50) : base(workItemClient, batchSize)
        {
            _doneState = doneState;
        }

        protected override async Task<WorkItemDoneDate> AggregatorAsync(WorkItem workItem, DateTime? sinceDate)
        {
            if (workItem?.Id == null)
                return null;

            var revisions = await WorkItemClient.GetWorkItemRevisionsAsync(workItem);
            var doneDate = revisions.LastStateTransitionTo(_doneState);

            if (doneDate != null && doneDate.Value.IsNewerThan(sinceDate))
                return new WorkItemDoneDate
                {
                    Id = workItem.Id.Value,
                    DoneDate = doneDate.Value
                };

            return null;
        }
    }
}