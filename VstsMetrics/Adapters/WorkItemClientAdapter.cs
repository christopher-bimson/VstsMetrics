using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using VstsMetrics.Abstractions;

namespace VstsMetrics.Adapters
{
    internal class WorkItemClientAdapter : IWorkItemClient
    {
        private readonly WorkItemTrackingHttpClient _witClient;

        public WorkItemClientAdapter(WorkItemTrackingHttpClient witClient)
        {
            if (witClient == null)
                throw new ArgumentNullException(nameof(witClient));

            _witClient = witClient;
        }

        public async Task<IEnumerable<WorkItemReference>> QueryWorkItemsAsync(string projectName, string fullyQualifiedQueryName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                throw new ArgumentException("This argument is required.", nameof(projectName));

            if (string.IsNullOrWhiteSpace(fullyQualifiedQueryName))
                throw new ArgumentException("This argument is required.", nameof(fullyQualifiedQueryName));

            var queryItem = await _witClient.GetQueryAsync(projectName, fullyQualifiedQueryName);

            return (await _witClient.QueryByIdAsync(queryItem.Id)).WorkItems;
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemRevisionsAsync(WorkItem workItem)
        {
            if (workItem == null)
                throw new ArgumentNullException(nameof(workItem));

            if (workItem.Id == null)
                throw new ArgumentException("It's not possible to retrive revisions of a work item that does not have an id.", 
                    nameof(workItem));

            return await _witClient.GetRevisionsAsync(workItem.Id.Value);
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsAsync(IEnumerable<int> workItemIds)
        {
            if (workItemIds == null || !workItemIds.Any())
                throw new ArgumentException("At least one work item id is required.", nameof(workItemIds));

            return await _witClient.GetWorkItemsAsync(workItemIds);
        }
    }
}
