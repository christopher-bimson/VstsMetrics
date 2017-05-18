using System.Collections.Generic;

namespace VstsMetrics.Renderers
{
    public interface IOutputRenderer
    {
        void Render<T>(IEnumerable<T> thingsToRender);
    }
}