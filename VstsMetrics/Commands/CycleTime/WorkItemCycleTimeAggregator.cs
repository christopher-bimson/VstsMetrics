using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Extensions;

namespace VstsMetrics.Commands.CycleTime
{
    public class WorkItemCycleTimeAggregator : WorkItemAggregator<WorkItemCycleTime>
    {
        private readonly string _initialState;
        private readonly string _toState;
        private readonly bool _strict;

        public WorkItemCycleTimeAggregator(string initialState, string toState, bool strict, WorkItemTrackingHttpClient workItemClient, int batchSize = 50) : base(workItemClient, batchSize)
        {
            _initialState = initialState;
            _toState = toState;
            _strict = strict;
        }

        protected override async Task<WorkItemCycleTime> AggregatorAsync(WorkItem workItem)
        {
            var revisions = await workItem.AllRevisionsAsync(WorkItemClient);

            DateTime? startTime;
            if (_strict)
            {
                startTime = revisions.FirstStateTransitionFrom(_initialState);
            }
            else
            {
                startTime = revisions.LastStateTransitionFrom(_initialState);
            }

            var endTime = revisions.LastStateTransitionTo(_toState);


            if (startTime != null && endTime != null)
                return new WorkItemCycleTime(workItem.Id.Value, workItem.Title(),  
                    workItem.Tags(), startTime.Value, endTime.Value);

            return null;
        }
    }
}
