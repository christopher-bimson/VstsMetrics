using System;
using System.Threading.Tasks;
using CommandLine;
using VstsMetrics.Abstractions;
using VstsMetrics.Renderers;

namespace VstsMetrics.Commands.CycleTime
{
    public class CycleTimeCommand : Command
    {
        [Option('i', "initialState", HelpText = "The state that work items leave to begin a cycle of work.")]
        public string InitialState { get; set; }
            
        [Option('e', "endState", HelpText = "The work item state that is the end of the work cycle.")]
        public string ToState { get; set; }

        [Option('s', "since", Required = false, DefaultValue = null,
            HelpText = "Only calculate the cycle time for work items that entered the end state on or after this date.")]
        public DateTime? Since { get; set; }

        [Option('x', "strict", Required = false, DefaultValue = false, 
            HelpText = "Controls how the start time of a work item is calculated. " +
            "When --strict is specified, the first state transition out of --initialState is used, " +
            "otherwise the last state transition out of the --initialState is used as the start time.")]
        public bool Strict { get; set; }

        [Option('d', "detailed", Required = false, DefaultValue = false, 
            HelpText = "When this flag is set, a detailed report on work item cycle time is produced. This is useful " +
            "for more detailed analysis in another tool. When not set, output is a high level summary of the work item cycle time.")]
        public bool Detailed { get; set; }

        public override async Task Execute()
        {
            var workItemClient = WorkItemClientFactory.Create(ProjectCollectionUrl, PatToken);

            var workItemReferences = await workItemClient.QueryWorkItemsAsync(ProjectName, Query);

            var calculator = new WorkItemCycleTimeAggregator(InitialState, ToState, Strict, workItemClient);
            var cycleTimes = await calculator.AggregateAsync(workItemReferences);

            var renderer = OutputRendererFactory.Create(OutputFormat);
            if (Detailed)
                renderer.Render(cycleTimes);      
            else
                renderer.Render(cycleTimes.Summarise());
        }

        public CycleTimeCommand(IWorkItemClientFactory workItemClientFactory, IOutputRendererFactory outputRendererFactory) 
            : base(workItemClientFactory, outputRendererFactory)
        {
        }
    }
}