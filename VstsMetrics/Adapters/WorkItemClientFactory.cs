using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using VstsMetrics.Abstractions;

namespace VstsMetrics.Adapters
{
    internal class WorkItemClientFactory : IWorkItemClientFactory
    {
        public IWorkItemClient Create(string projectCollectionUri, string patToken)
        {
            if (string.IsNullOrWhiteSpace(projectCollectionUri))
                throw new ArgumentException("This argument is required.", nameof(projectCollectionUri));

            if (string.IsNullOrWhiteSpace(patToken))
                throw new ArgumentException("This argument is required.", nameof(patToken));

            var connection = new VssConnection(new Uri(projectCollectionUri), new VssBasicCredential(string.Empty, patToken));
            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            return new WorkItemClientAdapter(witClient);
        }
    }
}
