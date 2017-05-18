using System;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using VstsMetrics.Extensions;

namespace VstsMetrics.Commands.Throughput
{
    public class ThroughputCommand : Command
    {
        [Option('d', "doneState", DefaultValue = "Done", HelpText = "The work item state you project is using to indicate a work item is 'Done'.")]
        public string DoneState { get; set; }

        public override async Task Execute()
        {
            var witClient = await GetWorkItemTrackingClient();
            var queryItem = await witClient.GetQueryAsync(ProjectName, Query);
            var workItemReferences = (await witClient.QueryByIdAsync(queryItem.Id)).WorkItems;

            var calculator = new WorkItemDoneDateAggregator(witClient, DoneState);
            var workItemDoneDates = (await calculator.AggregateAsync(workItemReferences));

            var weeklyThroughput = 
                workItemDoneDates
                    .GroupBy(t => t.DoneDate.StartOfWeek(DayOfWeek.Monday))
                    .OrderBy(t => t.Key)
                    .Select(g => new {WeekBeginning = g.Key, Throughput = g.Count()});

            Renderer.Render(weeklyThroughput);
        }
    }
}