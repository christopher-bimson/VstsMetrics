using CommandLine;
using CommandLine.Text;
using VstsMetrics.Commands.CycleTime;
using VstsMetrics.Commands.Throughput;

namespace VstsMetrics.Commands
{
    public class Options
    {
        [VerbOption("throughput", HelpText = "Calculates work item throughput from a predefined work item query.")]
        public ThroughputCommand ThroughputCommand { get; set; }

        [VerbOption("cycleTime", HelpText = "Calculates work item cycle time from a predefined work item query.")]
        public CycleTimeCommand CycleTimeCommand { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
