using VstsMetrics.Commands;

namespace VstsMetrics.Renderers
{
    public interface IOutputRendererFactory
    {
        IOutputRenderer Create(MetricOutputFormat outputFormat);
    }
}