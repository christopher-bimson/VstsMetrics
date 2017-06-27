using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VstsMetrics.Abstractions
{
    public interface IWorkItemClient
    {
        Task<IEnumerable<WorkItemReference>> QueryWorkItemsAsync(string projectName, string fullyQualifiedQueryName);

        Task<IEnumerable<WorkItem>> GetWorkItemRevisionsAsync(WorkItem workItem);

        Task<IEnumerable<WorkItem>> GetWorkItemsAsync(IEnumerable<int> workItemIds);
    }
}
