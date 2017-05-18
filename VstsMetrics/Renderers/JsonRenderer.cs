using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace VstsMetrics.Renderers
{
    public class JsonRenderer : IOutputRenderer
    {
        private readonly TextWriter _output;

        private readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };


        public JsonRenderer(TextWriter output)
        {
            _output = output;
        }

        public void Render<T>(IEnumerable<T> thingsToRender)
        {
            var serializedThings = JsonConvert.SerializeObject(thingsToRender, _jsonSerializerSettings);
            _output.Write(serializedThings);
            _output.WriteLine();
        }
    }
}