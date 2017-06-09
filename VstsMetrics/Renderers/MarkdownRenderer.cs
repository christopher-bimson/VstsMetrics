using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using VstsMetrics.Extensions;

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
