using System;
using System.Threading.Tasks;
using Nito.AsyncEx;
using VstsMetrics.Commands;

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

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
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
