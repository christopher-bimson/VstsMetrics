using System.Collections.Generic;
using ConsoleTables;
using VstsMetrics.Extensions;

namespace VstsMetrics.Renderers
{
    public class PrettyPrintRenderer : IOutputRenderer
    {
        public void Render<T>(IEnumerable<T> thingsToRender)
        {
            //Doesn't look like we can inject any old TextWriter into this. Shame.          
            var table = ConsoleTable.From(thingsToRender);
            for (var i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i] = table.Columns[i].ToString().PascalCaseToTitleCase();
            }
            table.Options.EnableCount = false;
            table.Write();
        }
    }
}