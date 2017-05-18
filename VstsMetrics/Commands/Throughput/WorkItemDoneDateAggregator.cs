using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Extensions;

namespace VstsMetrics.Commands.Throughput
{
    public class WorkItemDoneDateAggregator : WorkItemAggregator<WorkItemDoneDate>
    {
        private readonly string _doneState;

        public WorkItemDoneDateAggregator(WorkItemTrackingHttpClient workItemClient, string doneState, 
            int batchSize = 50) : base(workItemClient, batchSize)
        {
            _doneState = doneState;
        }

        protected override async Task<WorkItemDoneDate> AggregatorAsync(WorkItem workItem)
        {
            if (workItem?.Id == null)
                return null;

            var revisions = await workItem.AllRevisionsAsync(WorkItemClient);
            var doneDate = revisions.LastStateTransitionTo(_doneState);

            if (doneDate == null)
                return null;

            return new WorkItemDoneDate
            {
                Id = workItem.Id.Value,
                DoneDate = doneDate.Value
            };
        }
    }
}