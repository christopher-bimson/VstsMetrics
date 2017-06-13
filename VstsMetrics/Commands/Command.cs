using System;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using VstsMetrics.Renderers;

namespace VstsMetrics.Commands
{
    public abstract class Command : ICommand
    {
        private static readonly IDictionary<MetricOutputFormat, IOutputRenderer> OutputRendererFactory
            = new Dictionary<MetricOutputFormat, IOutputRenderer>
            {
                { MetricOutputFormat.Pretty, new PrettyPrintRenderer() },
                { MetricOutputFormat.Csv, new CsvRenderer(Console.Out) },
                { MetricOutputFormat.Json, new JsonRenderer(Console.Out) },
                { MetricOutputFormat.Markdown, new MarkdownRenderer() }
            };


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

        protected IOutputRenderer Renderer
        {
            get { return OutputRendererFactory[OutputFormat]; }
        }

        public abstract Task Execute();

        protected async Task<WorkItemTrackingHttpClient> GetWorkItemTrackingClient()
        {
            var connection = new VssConnection(new Uri(ProjectCollectionUrl), new VssBasicCredential(string.Empty, PatToken));
            var witClient = await connection.GetClientAsync<WorkItemTrackingHttpClient>();
            return witClient;
        }
    }
}