using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VstsMetrics.Extensions;

namespace VstsMetrics.Renderers
{
    public class CsvRenderer : IOutputRenderer
    {
        private readonly TextWriter _output;

        public CsvRenderer(TextWriter output)
        {
            _output = output;
        }

        public void Render<T>(IEnumerable<T> thingsToRender)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            _output.WriteLine(ToCsvHeadingString<T>(properties));
            foreach (var thing in thingsToRender)
            {
                var csvString = ToCsvString(properties, thing);
                _output.WriteLine(csvString);
            }
        }

        private static string ToCsvHeadingString<T>(PropertyInfo[] properties)
        {
            return string.Join(",", properties.Select(pi => pi.Name).Select(name => name.PascalCaseToTitleCase()));
        }

        private static string ToCsvString<T>(PropertyInfo[] properties, T thing)
        {
            var thingString = new List<string>();
            foreach (var propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(thing).ToString().Replace(",", ";");
                thingString.Add(propertyValue);
            }
            var csvString = string.Join(",", thingString);
            return csvString;
        }
    }
}