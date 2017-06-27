using System;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using VstsMetrics.Abstractions;
using VstsMetrics.Extensions;
using VstsMetrics.Renderers;

namespace VstsMetrics.Commands.Throughput
{
    public class ThroughputCommand : Command
    {
        [Option('d', "doneState", DefaultValue = "Done", HelpText = "The work item state you project is using to indicate a work item is 'Done'.")]
        public string DoneState { get; set; }

        [Option('s', "since", Required = false, DefaultValue = null, HelpText = "Only calculate throuput for work items that entered the done state on or after this date.")]
        public DateTime? Since { get; set; }

        public override async Task Execute()
        {
            var workItemClient = WorkItemClientFactory.Create(ProjectCollectionUrl, PatToken);
            var workItemReferences = await workItemClient.QueryWorkItemsAsync(ProjectName, Query);

            var calculator = new WorkItemDoneDateAggregator(workItemClient, DoneState);
            var workItemDoneDates = await calculator.AggregateAsync(workItemReferences);

            var weeklyThroughput = 
                workItemDoneDates
                    .GroupBy(t => t.DoneDate.StartOfWeek(DayOfWeek.Monday))
                    .OrderByDescending(t => t.Key)
                    .Select(g => new {WeekBeginning = g.Key, Throughput = g.Count()});

            OutputRendererFactory.Create(OutputFormat).Render(weeklyThroughput);
        }

        public ThroughputCommand(IWorkItemClientFactory workItemClientFactory, IOutputRendererFactory outputRendererFactory) 
            : base(workItemClientFactory, outputRendererFactory)
        {
        }
    }
}