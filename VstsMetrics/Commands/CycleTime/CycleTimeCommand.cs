using System.Threading.Tasks;
using CommandLine;

namespace VstsMetrics.Commands.CycleTime
{
    public class CycleTimeCommand : Command
    {
        [Option('i', "initialState", HelpText = "The state that work items leave to begin a cycle of work.")]
        public string InitialState { get; set; }

        [Option('e', "endState", HelpText = "The work item state that is the end of the work cycle.")]
        public string ToState { get; set; }

        [Option('x', "strict", Required = false, DefaultValue = false, 
            HelpText = "Controls how the start time of a work item is calculated. " +
            "When --strict is specified, the first state transition out of --initialState is used, " +
            "otherwise the last state transition out of the --initialState is used as the start time.")]
        public bool Strict { get; set; }

        public override async Task Execute()
        { 
            var witClient = await GetWorkItemTrackingClient();

            var queryItem = await witClient.GetQueryAsync(ProjectName, Query);
            var workItemReferences = (await witClient.QueryByIdAsync(queryItem.Id)).WorkItems;

            var calculator = new WorkItemCycleTimeAggregator(InitialState, ToState, Strict, witClient);
            var cycleTimes = await calculator.AggregateAsync(workItemReferences);

            Renderer.Render(cycleTimes);            
        }
    }
}