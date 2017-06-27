using System;
using System.Collections.Generic;
using VstsMetrics.Commands;

namespace VstsMetrics.Renderers
{
    public class OutputRendererFactory : IOutputRendererFactory
    {
        private static readonly IDictionary<MetricOutputFormat, IOutputRenderer> OutputRendererDictionary
            = new Dictionary<MetricOutputFormat, IOutputRenderer>
            {
                {MetricOutputFormat.Pretty, new PrettyPrintRenderer()},
                {MetricOutputFormat.Csv, new CsvRenderer(Console.Out)},
                {MetricOutputFormat.Json, new JsonRenderer(Console.Out)},
                {MetricOutputFormat.Markdown, new MarkdownRenderer()}
            };

        public IOutputRenderer Create(MetricOutputFormat outputFormat)
        {
            if (!OutputRendererDictionary.ContainsKey(outputFormat))
                throw new ArgumentOutOfRangeException(nameof(outputFormat), $"No renderer for output format {outputFormat} is defined.");

            return OutputRendererDictionary[outputFormat];
        }
    }
}
