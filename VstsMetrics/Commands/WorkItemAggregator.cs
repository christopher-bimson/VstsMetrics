using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Abstractions;

namespace VstsMetrics.Commands
{
    public abstract class WorkItemAggregator<T>
    {
        protected readonly IWorkItemClient WorkItemClient;
        protected readonly int BatchSize;

        protected WorkItemAggregator(IWorkItemClient workItemClient, int batchSize = 50)
        {
            WorkItemClient = workItemClient;
            BatchSize = batchSize;
        }

        public async Task<IEnumerable<T>> AggregateAsync(IEnumerable<WorkItemReference> workItemReferences, DateTime? sinceDate = null)
        {
            var results = new List<T>();
            if (workItemReferences.Any())
            {
                var skip = 0;
                int[] workItemIds;
                do
                {
                    workItemIds = workItemReferences.Skip(skip).Take(BatchSize).Select(i => i.Id).ToArray();
                    if (workItemIds.Any())
                    {
                        var workItems = await WorkItemClient.GetWorkItemsAsync(workItemIds);
                        foreach (var workItem in workItems)
                        {
                            var aggregatorResult = await AggregatorAsync(workItem, sinceDate);
                            if (aggregatorResult != null)
                                results.Add(aggregatorResult);
                        }
                    }
                    skip += BatchSize;
                } while (ThereAreStillWorkItemsToProcess(workItemIds));
            }
            return results;
        }

        protected abstract Task<T> AggregatorAsync(WorkItem workItem, DateTime? sinceDate);

        private bool ThereAreStillWorkItemsToProcess(int[] workItemIds)
        {
            return workItemIds.Length == BatchSize;
        }

        protected bool IsNewerThan(DateTime stateTransitionDate, DateTime? sinceDate)
        {
            if (sinceDate == null)
                return true;

            return stateTransitionDate >= sinceDate;
        }
    }
}