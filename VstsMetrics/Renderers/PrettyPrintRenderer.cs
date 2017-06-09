using System.Collections.Generic;

namespace VstsMetrics.Renderers
{
    public class PrettyPrintRenderer : ConsoleTablesRenderer
    {
        public override void Render<T>(IEnumerable<T> thingsToRender)
        {
            var table = CreateTable(thingsToRender);
            table.Write();
        }
    }
}