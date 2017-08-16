using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstsMetrics.Abstractions;
using VstsMetrics.Renderers;

namespace VstsMetrics.Commands.StateTransitions
{
    public class StateTransitionsCommand : Command
    {
        public StateTransitionsCommand(IWorkItemClientFactory workItemClientFactory, IOutputRendererFactory outputRendererFactory) 
            : base(workItemClientFactory, outputRendererFactory)
        {
        }

        public override async Task Execute()
        {
            var workItemClient = await WorkItemClientFactory.Create(ProjectCollectionUrl, PatToken);
            var workItemReferences = await workItemClient.QueryWorkItemsAsync(ProjectName, Query);
            var aggregator = new WorkItemStateTransitionAggregator(workItemClient);

            var stateTransitions = await aggregator.AggregateAsync(workItemReferences);
            var renderableStateTransitions = new List<WorkItemStateTransition>();
            foreach (var setOfTransitions in stateTransitions)
            {
                    renderableStateTransitions.AddRange(setOfTransitions.Flatten());
            }

            var renderer = OutputRendererFactory.Create(OutputFormat);
            renderer.Render(renderableStateTransitions);
        }
    }
}
