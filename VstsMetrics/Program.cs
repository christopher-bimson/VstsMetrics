using System;
using System.Globalization;
using System.Threading.Tasks;
using CommandLine;
using Nito.AsyncEx;
using VstsMetrics.Adapters;
using VstsMetrics.Commands;
using VstsMetrics.Renderers;

namespace VstsMetrics
{
    class Program
    {

        static int Main(string[] args)
        {
            try
            {
                return AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        static async Task<int> MainAsync(string[] args)
        {
            ICommand command = null;

            var parser = new Parser(with =>
            {
                with.ParsingCulture = CultureInfo.CurrentCulture;
                with.HelpWriter = Console.Out;
            });

            var options = new Options(new WorkItemClientFactory(), new OutputRendererFactory());
            if (!parser.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  command = (ICommand)subOptions;
              }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            await command.Execute();
            return 0;
        }
    }
}
