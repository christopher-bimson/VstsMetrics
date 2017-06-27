using System;
using System.Threading.Tasks;
using CommandLine;
using VstsMetrics.Abstractions;
using VstsMetrics.Renderers;

namespace VstsMetrics.Commands
{
    public abstract class Command : ICommand
    {
        [Option('u', "projectUrl", Required = true,
            HelpText =
                "The URL to the TFS/VSTS project collection. E.g. for VSTS: https://{your-account}.visualstudio.com")]
        public string ProjectCollectionUrl { get; set; }

        [Option('t', "pat", Required = true,
            HelpText = "Your personal authentication token. Used to authenticate to the VSTS REST API.")]
        public string PatToken { get; set; }

        [Option('p', "projectName", Required = true, HelpText = "The name of the team project you want to query.")]
        public string ProjectName { get; set; }

        [Option('q', "query", Required = true,
            HelpText = "The full path to the query that will return the work items you want to gather metrics on.")]
        public string Query { get; set; }

        [Option('o', "outputFormat", Required = false, DefaultValue = MetricOutputFormat.Pretty, 
            HelpText = "Valid output formats are Pretty, JSON, CSV and Markdown.")]
        public MetricOutputFormat OutputFormat { get; set; }

        protected IWorkItemClientFactory WorkItemClientFactory { get; private set; }

        protected IOutputRendererFactory OutputRendererFactory { get; private set; }

        protected Command(IWorkItemClientFactory workItemClientFactory, IOutputRendererFactory outputRendererFactory)
        {
            if (workItemClientFactory == null)
                throw new ArgumentNullException(nameof(workItemClientFactory));

            if (outputRendererFactory == null)
                throw new ArgumentNullException(nameof(outputRendererFactory));

            WorkItemClientFactory = workItemClientFactory;
            OutputRendererFactory = outputRendererFactory;
        }

        public abstract Task Execute();
    }
}