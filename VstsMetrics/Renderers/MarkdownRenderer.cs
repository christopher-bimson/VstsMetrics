using System;
using System.Collections.Generic;

namespace VstsMetrics.Renderers
{
    public class MarkdownRenderer : ConsoleTablesRenderer
    {
        public override void Render<T>(IEnumerable<T> thingsToRender)
        {
            var table = CreateTable(thingsToRender);
            Console.Write(table.ToMarkDownString());
        }
    }
}
