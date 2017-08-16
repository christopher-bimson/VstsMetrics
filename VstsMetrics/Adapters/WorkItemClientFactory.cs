using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using VstsMetrics.Abstractions;

namespace VstsMetrics.Adapters
{
    internal class WorkItemClientFactory : IWorkItemClientFactory
    {
        public async Task<IWorkItemClient> Create(string projectCollectionUri, string patToken)
        {
            if (string.IsNullOrWhiteSpace(projectCollectionUri))
                throw new ArgumentException("This argument is required.", nameof(projectCollectionUri));

            if (string.IsNullOrWhiteSpace(patToken))
                throw new ArgumentException("This argument is required.", nameof(patToken));

            var connection = new VssConnection(new Uri(projectCollectionUri), new VssBasicCredential(string.Empty, patToken));
            var witClient = await connection.GetClientAsync<WorkItemTrackingHttpClient>();
            return new WorkItemClientAdapter(witClient);
        }
    }
}
